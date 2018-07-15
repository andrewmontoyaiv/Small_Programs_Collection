TITLE Homework 4 Question 2             (H4Q2.asm)

;	name: Andrew Montoya
;	date: 4/5/2017
;	descr:
;			displays a single character at 100 random screen locations. 


INCLUDE Irvine32.inc

CHAR_VAL = 'A'
COUNT = 100

.data
	rows BYTE ?
	cols BYTE ?
.code
main PROC
	sub eax, eax
	mov  ecx,COUNT			; character count
	call GetMaxXY

	mov rows,al
	mov cols,dl
	call crlf


L1:	
	mov al,rows				;rows
	call RandomRange
	mov  dh,al

	mov  al,cols			;cols
	call RandomRange
	mov  dl,al
	call Gotoxy				; locate cursor
	mov  al,CHAR_VAL		; display the character
	call WriteChar
	call PushDelay

	loop L1					; next character

	mov dx,0				; move cursor to 0,0
	call Gotoxy

	exit
main ENDP


PushDelay PROC
	push eax

	mov  eax,100
	call Delay	; pause the program (EAX = milliseconds)

	pop  eax
	ret
PushDelay ENDP

END main
