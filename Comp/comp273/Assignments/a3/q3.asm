.globl main
  	
  .data
  first_name:	.space 20
  last_name:	.space 20
  limit:		20
  
  askf:	.asciiz "What is your first name?\n"
  askl:	.asciiz "What is your last name?\n"
  
  enter:	.asciiz "You entered "
  comma:	.asciiz ", "
  point:	.asciiz ".\n"
  
  
  #code from lectures slides.
  .text
  		 main:	
  		sub	$sp, $sp, 4	# push the first prompt onto the stack
  		la	$t1, askf
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  
  		# call gets(first_name, 20)
  		sub 	$sp, $sp, 4	# push the limit onto the stack
  		lw	$t1, limit
  		sw	$t1, ($sp)
  		
  		sub	$sp, $sp, 4	# ask for first name
  		la	$t1, first_name
  		sw	$t1, ($sp)
  		
  		jal	gets
  		add	$sp, $sp, 8
  		
  		# call puts(askl)
  		sub	$sp, $sp, 4	#ask for last name
  		la	$t1, askl
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  
  		# gets(last_name, 20)
  		sub 	$sp, $sp, 4	# push the limit onto the stack
  		lw	$t1, limit
  		sw	$t1, ($sp)
  		
  		sub	$sp, $sp, 4	# push the first name pointer onto the stack
  		la	$t1, last_name
  		sw	$t1, ($sp)
  		
  		jal	gets
  		add	$sp, $sp, 8
  		
  		# puts(enter)
  		sub 	$sp, $sp, 4	# push the third prompt onto the stack
  		la	$t1, enter
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  		
  		# cputs(first_name)
  		sub 	$sp, $sp, 4	# push the first_name prompt onto the stack
  		la	$t1, first_name
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  		
                #puts(comma)
  		sub 	$sp, $sp, 4	# push the forth prompt onto the stack
  		la	$t1, comma
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  		
  		# puts(last_name)
  		sub 	$sp, $sp, 4	# push the last_name prompt onto the stack
  		la	$t1, last_name
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  		
  		# call puts(prompt_5)
  		sub 	$sp, $sp, 4	# push the third prompt onto the stack
  		la	$t1, point
  		sw	$t1, ($sp)
  		
  		jal	puts
  		add	$sp, $sp, 4
  		#end the program
  		li	$v0, 10 #system call to end 
  		syscall		
  		
  #--------------------------------------------------------------------	
  
  getchar: 	lui	$a3, 0xffff	# store base address of mapped device in a3
  ckgetchar:	lw	$t1, ($a3)	# load the control register of the keyboard
  		andi	$t1, $t1, 0x1	# check the flag
  		beqz	$t1, ckgetchar	# not set, loop
  		lw	$v0, 4($a3)	# else, getchar
  		jr	$ra		# return
  		
  #-----------------------------------------------------------------------------		
  # putchar driver; argument passed via $a0
  putchar:	lui	$a3, 0xffff	# store base address of mapped device in a3
  ckputchar:	lw	$t1, 8($a3)	# load the control register of the screen
  		andi	$t1, $t1, 0x1	# check the flag
  		beqz	$t1, ckputchar	# not set, loop
  		sb	$a0, 12($a3)	# else, putchar
  		jr	$ra		# return
  
  
  #-----------------------------------------------------------------
  # void gets(char* buf, int size);		
  gets:		sub 	$sp, $sp, 4	# push $ra onto the stack
  		sw	$ra, ($sp)	
  		
  		sub 	$sp, $sp, 4	# push $fp onto the stack
  		sw	$fp, ($sp)
  		
  		move	$fp, $sp	# words at fp: old fp (0) / ra (4) / buf (8) / limit (12)
  		
  		lw	$t1, 12($fp)	# load the limit
  		subi	$t1, $t1, 1	# subtract 1 from the limit, to leave space for a null terminator.
  		sw	$t1, 12($fp)	# save the limit
  		
  		sub	$sp, $sp, 4	# allocate one word for a counter on the stack
  		li	$t1, 0		# initialize counter to zero
  		sw	$t1, 0($sp)	# write that to the stack
  		
  lp_gets:	lw	$t1, 0($sp)		# load counter
  		lw	$t2, 12($fp)		# load limit
  		bge	$t1, $t2, lp_gets_end	# if counter >= limit: goto end
  		
  		jal	getchar			# read a character
  		beq	$v0, '\n', lp_gets_end	# if we hit a line break: goto end
  		
  		lw	$t1, 0($sp)		# load counter
  		lw	$t2, 8($fp)		# load buf pointer
  		add	$t1, $t1, $t2		# add buf pointer to counter offset to make a pointer to the destination char
  		
  		sb	$v0, ($t1)	# write the character to the destination buffer
  		
  		lw	$t1, 0($sp)	# load counter
  		addi	$t1, $t1, 1	# increase counter by 1
  		sw	$t1, 0($sp)	# save counter
  		j	lp_gets		# loop
  		
  		lw	$t1, 0($sp)	# load counter
  		lw	$t2, 8($fp)	# load buf pointer
  		add	$t1, $t1, $t2	# compute address of last char in buf, or char that would have been \n
  		li	$t2, '\0'	# load a null
  		sw	$t2, ($t1)	# write the null at the end of buf
  		
  lp_gets_end:	lw	$ra, 4($fp)	# load return address
  		lw	$fp, 0($fp)	# restore $fp
  		add	$sp, $sp, 12	# restore the stack
  		jr	$ra		# return
  
  #---------------------------------------------------------------------------------------------------		
  # void puts(char* buf)
  puts:		sub	$sp, $sp, 4		# push return address onto the stack
  		sw	$ra, ($sp)
  		
  		sub 	$sp, $sp, 4		# push the frame pointer onto the stack
  		sw	$fp, ($sp)
  		
  		# begin new stack frame
  		move	$fp, $sp	
  		
  		sub	$sp, $sp, 4	# allocate counter
  		li	$t1, 0		# counter = 0
  		sw	$t1, ($sp)
  		
  lp_puts:	lw	$t1, ($sp)	# load the counter
  		lw	$t2, 8($fp)	# load the buffer pointer
  		add	$t1, $t1, $t2	# compute the address of the char to write
  		
  		lbu	$a0, ($t1)	# load the char to write into the driver argument 1 register
  		
  		beq 	$a0, '\0', lp_puts_end	# if the char to write is a null, then goto end.
  		
  		jal	putchar		# else we call the putchar driver
  		
  		lw	$t1, ($sp)	# load the counter
  		addi	$t1, $t1, 1	# increment the counter
  		sw	$t1, ($sp)	# save the counters
  		
  		j 	lp_puts		# and loop
  		
  lp_puts_end:	move	$sp, $fp	# restore the stack pointer
  		lw	$ra, 4($fp)	# restore the return address
  		lw	$fp, ($fp)	# restore the frame pointer
  		jr	$ra		# return
  	
  			
 
  #------------------------------------------------		
  		 
