
;	Create an array of size 100000.
;	Displays all prime number based on the input in the range of 2 - 64999


INCLUDE Irvine32.inc

.data
		
		count DWORD 0
		current DWORD 0
		MAXarr = 100000
		Arr DWORD MAXarr DUP(?)
		intro BYTE "Enter a value between 0 and 100000" , 13, 10, 0
		error BYTE "ERROR. Enter another value.", 13, 10, 0
		doAgain BYTE 13, 10, "Again?  Enter 1 for YES or 0 for NO", 13, 10, 0
		bye BYTE "Bye", 13, 10, 0

		MAX = 3							 ;max chars to read
		stringIn BYTE MAX+1 DUP (?)		 ;room for null

.code
main PROC
doOver:									; Print Message
		XOR eax, eax
		MOV edx, OFFSET intro
		CALL WriteString

goAgain:
		CALL ReadInt					; Repeat until valid message
		JS jump
		CMP eax, 0h
		JE jump
		CMP eax, 100000
		JG jump



		JMP goNext

jump:
		MOV edx, OFFSET error
		CALL WriteString
		JMP goAgain



goNext:
		MOV [count], eax 
		XOR eax, eax


		XOR eax, eax


		mov al, 0						; value to be stored
		mov edi,OFFSET Arr				; ES:DI points to target

		
		mov ecx, [count]				; character count
		cld								; direction = forward
		rep stosb						; fill

		MOV eax, 2
nextNumber:
		MOV current, eax				; store index value

		CMP eax, 650					; after looping tp 650d, will begin to print primes
		JA print

		CMP [Arr+4*eax], 0				; array index == 0?
		JE check						; yes, check if prime
		INC eax							
		JMP nextNumber					; no, unconditional loop to next number

check:
		XOR ebx, ebx					; start ebx indexing
		MOV ebx, 2
primeTest:
		CMP ebx, eax					; ebx >= eax?
		JAE passed						; yes, go to passed

		XOR edx, edx					; clear edx for mulptiplication
		DIV ebx							; divide eax with ebx

		CMP edx, 0						; remainder == 0?
		JE notPrime						; yes, number is not prime
		INC ebx							; next test number
		MOV eax, current				; reset eax
		JMP primeTest					; next ebx test

notPrime:
		MOV eax, current				; reset eax to current
		MOV [Arr+4*eax], 1				; not prime, so set current index to 1
		INC eax							; increment eax for next number
		JMP nextNumber					; jump to nextNumber

passed:
		MOV ebx, 2						; divisor
markValues:
		MOV eax, current				; reset eax
		MUL ebx							; eax * ebx
		MOV [Arr+4*eax], 1				; not prime, set to 1


		CMP eax, [count]					; result == count?
		JAE done						; yes, move to next number

		INC ebx							
		JMP markValues

done:
		MOV eax, current				;  reset eax to current
		INC eax							; move eax to next indexing value
		JMP nextNumber			

print:
		XOR ecx, ecx					; clear eax
		MOV ecx, [count]					; set loop to count
		MOV eax, 1
loopArr:
		INC eax							; starting value = 2
		CMP [Arr+4*eax], 1				; indexed value = 1
		JE continue						; yes, don't print
		CALL WriteInt					; no, print value
		CALL printSpace
continue:	
		LOOP loopArr					; print again

		MOV edx, OFFSET doAgain			; Print message and ask again?
		CALL WriteString



		XOR eax, eax					; Read yes/no
		XOR edx, edx
		CALL ReadInt

		CMP ax, 1
		JE doOver						; input of 1, do over





		MOV edx, OFFSET bye
		CALL WriteString

printSpace:								; Print space between entries
		PUSH eax
		MOV al, 20h
		CALL Writechar
		POP eax
		ret




	exit
main ENDP

END main