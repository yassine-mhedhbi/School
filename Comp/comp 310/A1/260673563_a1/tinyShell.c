#define _GNU_SOURCE

#include<stdlib.h>
#include<string.h>
#include<readline/readline.h>
#include<readline/history.h>
#include<unistd.h>
#include<sys/wait.h>
#include<sys/types.h>
#include<linux/sched.h>
#include <fcntl.h>


char** split(char* input);

int clone_function (void* line);

int my_system(char* line,char* fifo, char mode);


int main(int argc, char **argv) {
	
	struct timespec start_time;
	struct timespec finish_time;

	char* cmd=NULL;
	char* mode=NULL ;
	char* fifo = NULL;
	size_t size = 100;
	char cwd[1000];
	if (argv[1]!= NULL) fifo = argv[1];
	if (argv[2]!= NULL) mode = argv[2];
	while(1) {
		// Get the current directory and print it.
		getcwd(cwd, sizeof(cwd));
		printf("\n>%s$ ", cwd); 

		// Get the line (terminate in case of an error)
		if (getline(&cmd, &size, stdin)==-1) break;
		
		// exit command
		if (!strcmp(cmd, "exit\n")) {break;} //exit
		else {
			clock_gettime(CLOCK_REALTIME, &start_time);
			my_system(cmd,fifo,mode[0]);
			clock_gettime(CLOCK_REALTIME, &finish_time);
			long ns = finish_time.tv_nsec - start_time.tv_nsec;
	
		printf("EXECUTION TIME : %ld (ns)\n",ns);

		}
	}

	return 0;

}

// only for clone, in order to make cd works, we need to split then use chdir
//online documentation show usage of strok with delim 
char** split(char* input){
	int index=0;
	char** args=malloc(64*sizeof(char*)Muthucumaru Maheswaran);
	char* arg;

	arg=strtok(input, " \t\r\n\a");
	while(arg!=NULL){
		//add to tokens and increment
		args[index++]=arg;
		arg=strtok(NULL, " :\t\r\n\a");
	}
	args[index]=NULL;
	return args;
}


int clone_function (void* line){
	char** cmd=split((char*) line);
	if(strcmp(cmd[0], "cd") == 0){
		// if command is CD
		if(chdir(cmd[1])<0){
			perror("cd error");
			_exit(1);
		}
	}
	// any other command
	else{
		if(execl("/bin/sh", "sh", "-c", line, (char*) 0)<0){ perror("ERROR");
		_exit(1);
		}
		_exit(0);
	}
}

int my_system(char* line,char* fifo, char mode){
	pid_t pid;
	pid_t wpid;
	int status;
	

	#ifdef FORK

	pid = fork();
	if(pid==0){
		//execute child process
		if(execl("/bin/sh", "sh", "-c", line, (char*) 0)<0) perror("ERROR");
		_exit(0);
		}
	else if (pid < 0) perror("ERROR: CANNOT FORK");
	else {	
		//wait for parent process
		wpid = waitpid(pid, &status, WUNTRACED);
	}



	#elif VFORK
	pid = vfork();
	if(pid==0){
		//execute child process
		if(execl("/bin/sh", "sh", "-c", line, (char*) 0)<0) perror("ERROR");
		_exit(0);
		}
	else if (pid < 0) perror("ERROR: CANNOT FORK");
	else {
		//wait for parent process
		wpid = waitpid(pid, &status, WUNTRACED);
	}


	#elif CLONE
	

	//setting up stack
	//defaut stack size
	int stacksize = 8192;
	void *stack = malloc(stacksize);
	if (stack == NULL){
		//malloc error
		exit(1);
	}

	//set the pointer
	void *p = stack + stacksize;


	// To perform cd, we need the file system.
	int flags = CLONE_VFORK | CLONE_FS; 
		

	// call clone to execute the clone_function.
	pid = clone(&clone_function, p, flags | SIGCHLD , (void*) line);

	if(pid<0){
		perror("clone error");
		exit(1);
	}//wait for parent process
	
	else  wpid=waitpid(pid, &status, WUNTRACED);



	#elif PIPE
	int fd;
	pid = fork();
	if(pid==0){
		// Redirect output and output based on the mode (argument by the user)
		if(mode=='r'){
	
				fd=open(fifo, O_RDONLY);
				close(0);
				dup2(fd, 0);
			}
		else if(mode=='w'){
				fd=open(fifo, O_WRONLY);
				close(1);
				dup2(fd, 1);
			}
		// Execute !
		if(execl("/bin/sh", "sh", "-c", line, (char*) 0)<0) perror("ERROR");
		_exit(1);
		}
	else if (pid < 0) perror("ERROR: CANNOT FORK");
	else {	
		//wait for parent process
		wpid = waitpid(pid, &status, WUNTRACED);
	}
	#else
	system(line);
	#endif
}
