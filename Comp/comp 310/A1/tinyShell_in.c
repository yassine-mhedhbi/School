#define _GNU_SOURCE

#include<stdlib.h>
#include<string.h>
#include<readline/readline.h>
#include<readline/history.h>
#include<unistd.h>
#include<sys/wait.h>
#include<sys/types.h>


const char *concat(char* s1, char* s2){
    char *ns = malloc(strlen(s1) + strlen(s2) + 1);
    ns[0] = '\0';
    strcat(ns, s1);
    strcat(ns, s2);
    return ns;
}


int mySystem(char* cmd,char*fifo){
	pid_t pid;
	
	pid_t wpid;
	int status;
	char* line=NULL;
	pid = fork();
	if (pid == 0) {
		if(execl("/bin/sh", "sh", "-c", cmd, (char*) 0)<0) perror("ERROR");
		_exit(1);
			//child: Execute the command
		close(0);
		line = read(open(fifo,"r","O_RDONLY"),line,sizeof(line));
		char* cm = concat(cmd,line);
		printf("%s,1",cm);
		printf("%s,2",cmd);
		printf("%s,3",line);
		
		
		
	}
	else if (pid < 0) perror("ERROR: CANNOT FORK");
	else {
		wpid = waitpid(pid, &status, WUNTRACED);
	}

}
int main(int argc, char **argv){
	char* cmd=NULL;
	size_t size = 100;
	char cwd[1024];
	char* fifo=NULL;
	if (argv[1] != NULL) fifo = argv[1];
	while(1) {
		getcwd(cwd, sizeof(cwd));
		printf("\nShell:%s$ ", cwd);
		if (getline(&cmd, &size, stdin)==-1) break;
		if (!strcmp(cmd, "exit\n")) {break;} //exit
		else {
			
			mySystem(cmd,fifo);
		}
	}

	return 0;
}


