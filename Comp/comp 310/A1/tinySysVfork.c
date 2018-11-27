define _GNU_SOURCE

#include<stdlib.h>
#include<string.h>
#include<readline/readline.h>
#include<readline/history.h>
#include<unistd.h>
#include<sys/wait.h>
#include<sys/types.h>




char** parse(char* line) {
	// Most code from documentation.
	// Initialize the list of tokens, initial buffersize and index of the list
	int BUFFS = 64;
	char* SEP = " \a\n\r\t";

	int size = BUFFS;
	int index = 0;
	char* tok;
	char** toks = malloc(size*sizeof(char*));

	if (!toks) perror("ERROR: CANNOT ALLOCATE MEMORY");

	// retrive the tokens
	tok = strtok(line, SEP);

	//Going through the tokens
	while(tok != NULL) {
		toks[index] = tok;
		index ++;
		// If more space needed
		if (index >= size) {
			// Reallcote and increase size.
			toks = realloc(toks, (size=size+BUFFS)*sizeof(char*));
			if (!toks) perror("ERROR: CANNOT ALLOCATE MEMORY");
			}
		// END
		tok = strtok(NULL, SEP);
		
	}
	toks[index] = NULL;
	return toks; 

}

int mySystem(char** cmd){
	pid_t pid;
	pid_t wpid;
	int status;

	pid = vfork();
	if (pid == 0) {
		//child: Execute the command
		int exec = execvp(cmd[0], cmd);
		if (exec < 0) perror("ERROR");
		exit(0);
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
	
	while(1) {
		getcwd(cwd, sizeof(cwd));
		printf("\nShell:%s$ ", cwd);
		if (getline(&cmd, &size, stdin)==-1) break;
		if (cmd[0] == '\0') {break;} //Error, empty line.
		else {
			
			mySystem(parse(cmd));
		}
	}

	return 0;
}


