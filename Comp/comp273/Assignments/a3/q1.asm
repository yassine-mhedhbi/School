.data 
	YEARS: 		.word 50
	Days: 		.word 0
.text	
	lw $a0 , YEARS		#load the value of years in s0
	lw $v1,  Days		# load 0 (Days) into s1
	
MyLoop:	beq $a0, $zero,else	#if (years = 0) then end loop
	add $v1, $v1, 365	#days =+ 365
	add $a0, $a0, -1	#years =-1
	j MyLoop		#jump back to loop.

else:	
	sw $v1, Days
	li $v0, 10		#system call to end the program
	syscall			#system call

	
	
