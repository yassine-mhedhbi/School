 
gcc tinyShell.c 
for system implementation

or 

gcc -D X tinyShell.c
where X is the implementation: FORK, VFORK, CLONE, PIPE

To run the PIPE shell:
First create fifo pipeline from terminal : 
1)$mkfifo myfifo
2) gcc -D PIPE tinyShell.c -o shell
2) ./shell myfifo [mode]

mode = r or w, w to write into the pipe, r to read from the pipe
I created the fifo pipeline with terminal cmd (mycourses discussion board) and included it in the files.
