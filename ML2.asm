sys_exit	equ	1
sys_read	equ	3
sys_write	equ	4
stdin		equ	0
stdout		equ	1
stderr		equ	3

section .data		;used to declare constants
	X	 db	'aaaaa'
	Y	 db	'aaaaa'
	Z	 db	'aaaaa'
	T1	 db	'aaaaa'
	T2	 db	'aaaaa'
	T3	 db	'aaaaa'
	T4	 db	'aaaaa'
	T5	 db	'aaaaa'
	T6	 db	'aaaaa'
	T7	 db	'aaaaa'
	T8	 db	'aaaaa'
	T9	 db	'aaaaa'
	T10	 db	'aaaaa'
	userMsg		db      'Enter an integer(less than 32,765): '
	lenUserMsg	equ	$-userMsg
	newline		db	0xA 	; 0xA 0xD is ASCII <LF><CR>
	Ten             DW      10  ;Used in converting to base ten.
	num		times 6 db 'ABCDEF'
	numEnd		equ	$-num
	Result          db      'Ans = '
	ResultValue	db	'aaaaa'
			db	0xA
	ResultEnd       equ 	$-Result    ; $=> here, subtract address Result

section .bss		;used to declare uninitialized variables

	TempChar        RESB    1              ;temp space for use by GetNextChar
	testchar        RESB    1
	ReadInt         RESW    1              ;Temporary storage GetAnInteger.	
	tempint         RESW	1              ;Used in converting to base ten.
	negflag         RESB    1              ;P=positive, N=negative

	global _start:		;main program
section .text

_start:
	call    PrintString
	call    GetAnInteger
	mov ax, [ReadInt]
	mov [T1], ax

	mov ax, [T1]
	mov [X], ax

	call    PrintString
	call    GetAnInteger
	mov ax, [ReadInt]
	mov [T2], ax

	mov ax, [T2]
	mov [Y], ax

	call    PrintString
	call    GetAnInteger
	mov ax, [ReadInt]
	mov [T3], ax

	mov ax, [T3]
	mov [Z], ax

	mov ax, [X]
	cmp ax, [Y]
	JLE L1

	mov ax, [X]
	cmp ax, [Z]
	JLE L2

	mov ax,[X]		;integer to print in ax
	call    ConvertIntegerToString  ;Convert binary integer to a char string
	mov ax,[X]		;integer to print in ax
	mov eax, 4	;write
	mov ebx, 1	;print default sys_out
	mov ecx, Result	;start address for print
	mov edx, ResultEnd
	int 80h

L2:	nop
L1:	nop
	mov ax, [Y]
	cmp ax, [X]
	JLE L3

	mov ax, [Y]
	cmp ax, [Z]
	JLE L4

	mov ax,[Y]		;integer to print in ax
	call    ConvertIntegerToString  ;Convert binary integer to a char string
	mov ax,[Y]		;integer to print in ax
	mov eax, 4	;write
	mov ebx, 1	;print default sys_out
	mov ecx, Result	;start address for print
	mov edx, ResultEnd
	int 80h

L4:	nop
L3:	nop
	mov ax, [Z]
	cmp ax, [X]
	JLE L5

	mov ax, [Z]
	cmp ax, [Y]
	JLE L6

	mov ax,[Z]		;integer to print in ax
	call    ConvertIntegerToString  ;Convert binary integer to a char string
	mov ax,[Z]		;integer to print in ax
	mov eax, 4	;write
	mov ebx, 1	;print default sys_out
	mov ecx, Result	;start address for print
	mov edx, ResultEnd
	int 80h

L6:	nop
L5:	nop

fini:
	mov eax,sys_exit ;terminate, sys_exit = 1
	xor ebx,ebx	;successfully, zero in ebx indicates success
	int 80h


PrintString:
	push    ax
	push    dx
	mov eax, 4
	mov ebx, 1		; print default output device
	mov ecx, userMsg	; pointer to string
	mov edx, lenUserMsg	; arg1, where to write, screen
	int	80h		; interrupt 80 hex, call kernel
	pop     dx              ;Restore registers.
	pop     ax
	ret

GetAnInteger:	;Get an integer as a string
	mov eax,3	;read
	mov ebx,2	;device
	mov ecx,num	;buffer address
	mov edx,6	;max characters
	int 0x80

ConvertStringToInteger:
	mov ax,0	;hold integer
	mov [ReadInt],ax ;initialize 16 bit number to zero
	mov ecx,num	;pt - 1st or next digit of number as a string 
	mov bx,0
	mov bl, byte [ecx] ;contains first or next digit
Next:	sub bl,'0'	;convert character to number
	mov ax,[ReadInt]
	mov dx,10
	mul dx		;eax = eax * 10
	add ax,bx
	mov [ReadInt], ax
	mov bx,0
	add ecx,1 	;pt = pt + 1
	mov bl, byte[ecx]
	cmp bl,0xA	;is it a <lf>
jne Next	; get next digit
ret


ConvertIntegerToString:
	mov ebx, ResultValue + 4   ;Store the integer as a five
ConvertLoop:
	sub dx,dx  ; repeatedly divide dx:ax by 10 to obtain last digit of number
	mov cx,10  ; as the remainder in the DX register.  Quotient in AX.
	div cx
	add dl,'0' ; Add '0' to dl to convert from binary to character.
	mov [ebx], dl
	dec ebx
	cmp ebx,ResultValue
	jge ConvertLoop

	ret
