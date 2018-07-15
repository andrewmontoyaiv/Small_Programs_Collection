TITLE Homework 6 Question 3				(H6Q3_2.asm)

;	Name: Andrew Montoya
;	Date: 5/9/2017
;	Revision: 2nd
;	Descr:
;		waits for a keystroke and returns the key that was pressed.   


INCLUDE Irvine32.inc
INCLUDE macros.inc

mReadkey MACRO ascii, scan
    scanKbd:
    mov  eax,50				; delay for reading keyboard input
    call Delay           

    call ReadKey			; look for keyboard input
    jz   scanKbd			; Key pressed? no, loop to scanKbd

	mov ascii, al			; pass al to ascii
	mov scan, ah			; pass ah to scan

	cmp    dx,VK_ESCAPE		; esc pressed?
    je    quit				; yes


ENDM

.data
	ascii BYTE ?
	scan BYTE ?
	str1 BYTE "ASCII code: ",0
	str2 BYTE "Scan code:  ",0
	str3 BYTE "ASCII representation is: ",0
	str4 BYTE "Press a key...",0

.code
main PROC

	mov edx, OFFSET str4	; press a key...
	call WriteString
	call Crlf


nextNumber:

XOR eax, eax
mReadkey ascii, scan
							; Display the values.

	mov edx, OFFSET str3	; Print ASCII representation
	call WriteString
	movzx eax, ascii
	call WriteChar
	call Crlf

    mov edx,OFFSET str1		; Print Ascii Code
    call WriteString
    call WriteHex
    call Crlf

    mov edx,OFFSET str2		; Print Scan code
    call WriteString
	XOR eax, eax
    movzx eax,scan
    call WriteHex
    call Crlf
	call Crlf

	loop nextNumber			; Keep repeating until esc is pressed
quit:

    exit
main ENDP
END main