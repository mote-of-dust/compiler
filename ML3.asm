sys_exit	equ	1
sys_read	equ	3
sys_write	equ	4
stdin		equ	0
stdout		equ	1
stderr		equ	3

section .data		;used to declare constants
	M	 db	'aaaaa'
	X	 db	'aaaaa'
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
	mov [M], ax

	mov ax, [M]
	add ax, 5
	mov [T2], ax

