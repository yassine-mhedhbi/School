	.data 
		promp:		.asciiz "Calculating the value of factorial\n"
		mess1: 		.asciiz "Enter a number:"
		mess2:		.asciiz "the Value of factorial is:"
		
	.text 
	main:
		li $v0, 4       	#system call code to print_str 
		la $a0, promp		#load the address of promp into the argument register
		syscall			#system call
		
		li $v0, 4		#system call code to print_str
		la $a0, mess1		#lowad address of mess1 into argument register
		syscall 		#system call
		
		li $v0, 5		#system call to read_int
		syscall			#system call
		
		move $a0, $v0		#move the read int into the argument
		jal factorial		#call factorial
		
		move $v1, $v0		#move the return value address $v1
		
		li	$v0, 4		#system call cod to print_str
		la	$a0, mess2	#load address of mess2 into argument register
		syscall			#system call
		
		li	$v0, 1		#system call code to print int
		la	$a0, ($v1)	#load content (using load address) into argument register
		syscall			#system call
		
		li 	$v0, 10		#systemm call code that program ends here
		syscall			#system call
		
factorial:	beq 	$a0, $zero, returnOne	# return 1 if (n==1)
		addi 	$sp, $sp, -8		#manipulate the stack
		sw 	$ra, 	4($sp)		#4($s) contains the return value
		sw 	$a0,	($sp) 		#put argument at top of stack
		
		addi 	$a0, $a0, -1		# n = n-1
		jal 	factorial 		# call factorial
		
		lw 	$ra, 4($sp)		#load the return address
		lw 	$a0,	0($sp)		#load n from the stack
		addi	$sp, 	$sp,8		#change the stack pointer
		
		mul	$v0,	$a0,$v0		#multiply n * fact(n-1)
		jr 	$ra			#jump back to parent (either fact or main)
		
returnOne:	addi 	$v0, $zero,1		#return 1
		jr 	$ra			#jump back to parent call
		

		
			
		
		
		
		
