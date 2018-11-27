#include<fcntl.h>
#include<sys/stat.h>
#include<sys/mman.h>
#include<stdio.h>
#include<unistd.h>
#include<string.h>

#define __TEST_MAX_KEY__  256
#define __TEST_MAX_KEY_SIZE__ 31
#define __TEST_MAX_DATA_LENGTH__ 256
#define __TEST_MAX_POD_ENTRY__ 256
#define __TEST_SHARED_MEM_NAME__ "/GTX_1080_TI"
#define __TEST_SHARED_SEM_NAME__ "/ONLY_TEARS"
#define __TEST_FORK_NUM__ 4



/*********************************************************************** 
*                                                                                                  *             
*  signature:  int kv_store_create(char *name)                           *
*  Description: Create a store, return 0 if it succeeded, -1 else. *
*                                                                                                  *
***********************************************************************/

int kv_store_create(char *name) {
	
	// Open/Create shared memory object.
	int fd = shm_open(kv_store_name, O_CREAT|O_RDWR, S_IRWXU);
	if (fd == -1) {
		perror("ERROR: Failed to create  shared memory \n");
		return -1;
	}
	
	// map the shared memory object at an address.
	void *addr = mmap(NULL, __KEY_VALUE_STORE_SIZE__, PROT_READ|PROT_WRITE, MAP_SHARED, fd, 0);

	if (addr == MAP_FAILED){
		perror("ERROR: mmap failed \n");
		return -1;
	}

	// resize the shared memory object 
	ftruncate(fd, __KEY_VALUE_STORE_SIZE__);

	// close fd 
	int  c = close(fd);
	if (c == -1){
		perror("Error: Cannot close the file descriptor \n");
		return -1;
	}

	
	// ------------ FOR SYCHRONIZATION -------------------- 

	int numProcess = 0;
	memcpy(addr, &numProcess, sizeof(int));


	struct pod podTemplate;

	podTemplate.nbFreeSlots = __KV_POD_MAX_NB_VALUES__;
	
	void *startOfPod = addr + sizeof(int);
	for (int i=0; i < __KV_POD_NB__; i++){
		memcpy(startOfPod, &podTemplate, sizeof(podTemplate));
		startOfPod = startOfPod + sizeof(podTemplate);
		//printf("i is %i, sizeof(int) is: %i", i, (int) sizeof(int));
		//fflush(stdout);
	}

	// delete mapping
	int munmapReturn = munmap(addr, __KEY_VALUE_STORE_SIZE__);
	
	// on failure it should return -1
	if(munmapReturn != 0){
		perror("munmap error in store creation \n");
		return -1;
		
		}

	// create two semaphores
	// O_EXCL makes sure that there are no semaphore of the same name before
	sem_t *semW = sem_open(__KV_WRITERS_SEMAPHORE__, O_CREAT|O_EXCL,0644,1);
	
	if (semW == SEM_FAILED){
		perror("Failing to create writer semaphore \n");
		return -1;
	}

	sem_t *semR = sem_open(__KV_READERS_SEMAPHORE__, O_CREAT|O_EXCL,0644,1);

	if (semR == SEM_FAILED){
		perror("Failing to create reader semaphore \n");
		return -1;
	}
	
	// clean up semaphores
	if (sem_close(semW) == -1 || sem_close(semR) == -1){
		perror ("Error closing semaphore in store creation. \n");
		return -1;
	}


	return 0;
}



/*************************************************************************

* 	 signature:  char *kv_store_read(char *key)                               *
* 	 Description:  return a value correspending to the key if found  *

*************************************************************************/

char *kv_store_read(char *key){

	struct stat s;
	int sizeOfInt = (int) sizeof(int);

	// limit key to 32 char 
	// truncate the strings to fit if longer string is provided as input
	int extraK = strlen(key) - (__MAX_KEY_SIZE__ - 1); // 31 b/c null char
	if(extraK > 0){
		key[strlen(key) - extraK] = 0;
	}




	// calculate hash of key and reduce it to the range of KVStore
	int kPodNb = (int) (generate_hash((unsigned char *) key) % __KV_POD_NB__); 


	// open read lock
	sem_t *semR = sem_open(__KV_READERS_SEMAPHORE__, 0);

	if(semR == SEM_FAILED){
		perror("Failed to open reader semaphore \n");
		return -1;
	}


	int sval;
	sem_getvalue(semR, &sval);


	// open write lock
	sem_t *semW = sem_open(__KV_WRITERS_SEMAPHORE__, 0);

	if(semW == SEM_FAILED){
		perror("Failed to open writer semaphore \n");
		return -1;
	}

	sem_getvalue(semW, &sval);
	printf("the value of semW is: %d\n", sval);
	fflush(stdout);

	// obtain readers' lock
	if(sem_wait(semR) != 0){
		perror("Failing sem_wait for readers, readers' lock \n");
		return -1;
	}

	// open the store
	int fd = shm_open(__KV_STORE_NAME__, O_RDWR, 0);

	// could fail if the system does not have enough memory, or user does not have proper permissions: return -1
	if (fd == -1){
		perror("Cannot open shm in write \n");
		return -1;
	}	
	
	if(fstat(fd, &s) == -1){
		perror("Error fstat in writer \n");
		return -1;
	}

	// map the shared memory object
	// NULL: let the kernal pick the start location
	void *addr = mmap(NULL, s.st_size, PROT_READ|PROT_WRITE, MAP_SHARED, fd, 0);

	// check if mmap gives an error
	if (addr == MAP_FAILED){
		perror("mmap error in store creation \n");
		return -1;
	}
	
	// close fd 
	int closeReturn = close(fd);
	if (closeReturn == -1){
		perror("Error closing file descriptor in store creation. \n");
		return -1;
	}
	

	// modifying read count and/or obtain write lock **********************************	

	int readCount = ++ *((int *) addr);
	


	// release lock
	if(sem_post(semR) != 0){
		perror("Error releasing readers' reader lock\n");
		return -1;
	}
	// end of modifying read count ****************************************************

	// read ---------------------------------------------------------------------------

	struct pod podInStore;

 	size_t podSize = sizeof(podInStore);

	// find pod
	struct pod *addrPod = addr + sizeOfInt + kPodNb*podSize;
	struct pod *originalAddrPod = addr + sizeOfInt + kPodNb*podSize;

	bool kFound = 0;

	podInStore = *addrPod;
	char *duplicatedValue = malloc(sizeof(char) * __MAX_VALUE_SIZE__);



	while(strlen(podInStore.key) != 0){


		// if the key is in the pod
		// 	duplicate the next value to read
		//	move the next value to read index
		if(strncmp(podInStore.key, key, strlen(key)) == 0){

			strcpy(duplicatedValue, podInStore.values[podInStore.nextRead]);
			addrPod->nextRead = (podInStore.nextRead + 1) % __KV_POD_MAX_NB_VALUES__;
			kFound = 1;
			break;
		}else{
			// if the key is not in the pod and pod not empty
			//	do linear probing until key found or empty slot met or whole table searched



			kPodNb = (kPodNb+1)%__KV_POD_NB__;
			addrPod = addr + sizeOfInt + kPodNb*podSize;
			podInStore = *addrPod;

			if (addrPod == originalAddrPod){
				break;
			}
		}	
	}


	// end of read --------------------------------------------------------------------

	// modifying read count and/or release write lock **********************************	

	// obtain readers' lock
	if(sem_wait(semR) != 0){
		perror("Failing sem_wait for readers, readers' lock \n");
		return -1;
	}


	readCount = -- *((int *) addr);
	
	// release reader's lock
	if(sem_post(semR) != 0){
		perror("Error releasing readers' reader lock\n");
		return -1;
	}

	// end of modifying read count ******************************************************


	// clean up
	// delete mapping
	int munmapReturn = munmap(addr, __KEY_VALUE_STORE_SIZE__);
	
	// on failure it should return -1
	if(munmapReturn != 0){
		perror("munmap error in writer \n");
		return -1;
	}

	// clean up semaphore
	if (sem_close(semW) == -1 || sem_close(semR) == -1){
		perror ("Error closing semaphores in reader. \n");
		return -1;
	}
	
	// returns a ptr to the string
	return duplicatedValue;
}


int main(int argc, char **argv) {

	return 0;
}