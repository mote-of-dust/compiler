sys_exit	equ	1
sys_read	equ	3
sys_write	equ	4
stdin		equ	0
stdout		equ	1
stderr		equ	3

section .data		;used to declare constants
	X	 db	?
	T1	 db	?
	T2	 db	?
	T3	 db	?
	T4	 db	?
	T5	 db	?
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
	mov eax, sys_write
	mov ebx, stdout
	mov ecx, userMsg
	mov edx, lenUserMsg
	int	80h

	mov	eax, sys_read
	mov	ebx, stdin
	mov 	ecx, [T1]
	mov 	edx, 6
	int	0x80

	mov ax, [T1]
	mov [X], ax

	mov ax,[X]		;integer to print in ax
	call    ConvertIntegerToString  ;Convert binary integer to a char string
	mov ax,[X]		;integer to print in ax
	mov eax, 4	;write
	mov ebx, 1	;print default sys_out
	mov ecx, Result	;start address for print
	mov edx, ResultEnd
	int 80h


fini:
	mov eax,sys_exit ;terminate, sys_exit = 1
	xor ebx,ebx	;successfully, zero in ebx indicates success
	int 80h


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
