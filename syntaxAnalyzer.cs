using System;
using System.Linq;

namespace app
{

    class syntaxAnalyzer
    {
        public static int tCounter = 0;
        public static int lCounter = 1;

        // find the precedence between current and previous token, and returns the symbol found.
        public String findSym(String prevTerm, String curToken, List<string[]> opPrecTab)
        {
            int colNum = 0;
            int rowNum = 0;
            string curTemp = "";
            string prevTemp = "";
            curTemp = curToken;
            prevTemp = prevTerm;
            if (curToken == ",")
            {
                curTemp = ";";
            }
            if (prevTerm == ",")
            {
                prevTemp = ";";
            }

            for (int i = 0; i < opPrecTab[0].GetLength(0); i++)
            {
                if (prevTemp == opPrecTab[0][i])
                {
                    colNum = i;
                }
                else
                {

                }
            }
            for (int i = 0; i < opPrecTab[0].GetLength(0); i++)
            {
                if (curTemp == opPrecTab[0][i])
                {
                    rowNum = i;
                }
                else
                {

                }
            }
            Console.WriteLine(" PrevToken: " + prevTerm + " " + opPrecTab[colNum][rowNum] + " curToken: " + curToken);

            return opPrecTab[colNum][rowNum];
        }

        public string popstack(List<string> pushdown, string prevTerm)
        {
            string tempo = "T";
            Console.WriteLine("Pre-pop: ");
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
            int opIndx = 0;
            List<string> popped = new List<string>();
            for (int i = (pushdown.Count() - 1); i >= 0; i--)
            {
                if (prevTerm == pushdown[i])
                {
                    opIndx = i;
                    break;
                }
            }
            Console.WriteLine("prevTerm: " + prevTerm + " found at index " + opIndx);
            popped.Add(pushdown[opIndx - 1]);
            popped.Add(pushdown[opIndx]);
            popped.Add(pushdown[opIndx + 1]);
            pushdown.RemoveAt((opIndx + 1));
            pushdown.RemoveAt(opIndx);
            pushdown.RemoveAt((opIndx - 1));



            Console.WriteLine("PSA after pop: ");
            if (prevTerm != "=")
            {
                tCounter++;
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                pushdown.Add(tempo + tCounter.ToString());
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown[(pushdown.Count) - 2];
            }
            else
            {
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown.Last();
            }
        }

        public string relopPop(List<string> pushdown, string prevTerm)
        {
            //string tempo = "L";
            Console.WriteLine("Pre-pop: ");
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
            int opIndx = 0;
            List<string> popped = new List<string>();
            for (int i = (pushdown.Count() - 1); i >= 0; i--)
            {
                if (prevTerm == pushdown[i])
                {
                    opIndx = i;
                    break;
                }
            }
            Console.WriteLine("prevTerm: " + prevTerm + " found at index " + opIndx);
            popped.Add(pushdown[opIndx - 1]);
            popped.Add(pushdown[opIndx]);
            popped.Add(pushdown[opIndx + 1]);
            pushdown.RemoveAt((opIndx + 1));
            pushdown.RemoveAt(opIndx);
            pushdown.RemoveAt((opIndx - 1));



            Console.WriteLine("PSA after pop: ");
            //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
            mlWriter(popped);
            //pushdown.Add(tempo + tCounter.ToString());
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("New prevTerm: " + pushdown[(pushdown.Count) - 1]);
            return pushdown[(pushdown.Count) - 1];

        }

        public string printPop(List<string> pushdown, string prevTerm)
        {
            Console.WriteLine("Pre-pop: ");
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
            int opIndx = 0;
            List<string> popped = new List<string>();
            for (int i = (pushdown.Count() - 1); i >= 0; i--)
            {
                if (prevTerm == pushdown[i])
                {
                    opIndx = i;
                    break;
                }
            }
            Console.WriteLine("prevTerm: " + prevTerm + " found at index " + opIndx);

            popped.Add(pushdown[opIndx]);
            popped.Add(pushdown[opIndx + 1]);
            pushdown.RemoveAt(opIndx + 1);
            pushdown.RemoveAt(opIndx);

            Console.WriteLine("PSA after pop: ");
            //Console.WriteLine("~~~~~~~~ PREVTERM: " + prevTerm);
            if (prevTerm != "=")
            {
                //tCounter++;
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown[(pushdown.Count) - 1];
            }
            else
            {
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown.Last();
            }
        }
        public string inputPop(List<string> pushdown, string prevTerm)
        {
            string tempo = "T";
            Console.WriteLine("Pre-pop: ");
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
            int opIndx = 0;
            List<string> popped = new List<string>();
            for (int i = (pushdown.Count() - 1); i >= 0; i--)
            {
                if (prevTerm == pushdown[i])
                {
                    opIndx = i;
                    break;
                }
            }
            Console.WriteLine("prevTerm: " + prevTerm + " found at index " + opIndx);

            popped.Add(pushdown[opIndx]);

            pushdown.RemoveAt(opIndx);



            Console.WriteLine("PSA after pop: ");
            //Console.WriteLine("~~~~~~~~ PREVTERM: " + prevTerm);
            if (prevTerm != "=")
            {
                tCounter++;
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                pushdown.Add(tempo + tCounter.ToString());
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown[(pushdown.Count) - 2];
            }
            else
            {
                //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE
                mlWriter(popped);
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown.Last();
            }
        }

        // popstack() will call this function once the popped list<string> is created. This function will append a file to
        public void mlWriter(List<string> popped)
        {
            TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.asm");
            // create a switch case based on item at index 1 (middle index)
            if (popped.Count == 3)
            {
                switch (popped[1])
                {
                    case "+":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tadd ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tadd ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tmov [T" + tCounter + "], ax\n");

                            break;
                        }
                    case "-":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tsub ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tsub ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tmov [T" + tCounter + "], ax\n");
                            break;
                        }
                    case "*":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\timul ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tmul byte [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tmov [T" + tCounter + "], ax\n");
                            break;
                        }
                    case "/":
                        {
                            tw.WriteLine("\tmov dx, 0");
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tmov bx, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov bx, [" + popped[2] + "]");
                            }
                            tw.WriteLine("\tdiv bx");

                            tw.WriteLine("\tmov [T" + tCounter + "], ax\n");
                            break;
                        }
                    case "=":
                        {

                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tmov [" + popped[0] + "], ax\n");

                            break;
                        }

                    case "==":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tcmp ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tcmp ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tJNE L" + lCounter + '\n');
                            lCounter++;
                            break;
                        }

                    case "!=":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tcmp ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tcmp ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tJE L" + lCounter + '\n');
                            lCounter++;
                            break;
                        }
                    case ">=":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tcmp ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tcmp ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tJL L" + lCounter + '\n');
                            lCounter++;
                            break;
                        }
                    case ">":
                        {
                            if (int.TryParse(popped[0], out _))
                            {
                                tw.WriteLine("\tmov ax, " + popped[0]);
                            }
                            else
                            {
                                tw.WriteLine("\tmov ax, [" + popped[0] + "]");
                            }
                            if (int.TryParse(popped[2], out _))
                            {
                                tw.WriteLine("\tcmp ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tcmp ax, [" + popped[2] + "]");
                            }

                            tw.WriteLine("\tJLE L" + lCounter + '\n');
                            lCounter++;
                            break;
                        }
                    default:
                        break;
                }
            }
            else if (popped.Count == 2)
            {
                switch (popped[0])
                {
                    case "PRINT":
                        {
                            tw.WriteLine("\tmov ax,[" + popped[1] + "]		;integer to print in ax");
                            tw.WriteLine("\tcall    ConvertIntegerToString  ;Convert binary integer to a char string");
                            tw.WriteLine("\tmov ax,[" + popped[1] + "]		;integer to print in ax");
                            tw.WriteLine("\tmov eax, 4	;write");
                            tw.WriteLine("\tmov ebx, 1	;print default sys_out");
                            tw.WriteLine("\tmov ecx, Result	;start address for print");
                            tw.WriteLine("\tmov edx, ResultEnd");
                            tw.WriteLine("\tint 80h\n");

                            break;
                        }
                }

            }
            else if (popped.Count == 1)
            {
                switch (popped[0])
                {
                    case "INPUT":
                        {
                            tw.WriteLine("\tcall    PrintString");
                            tw.WriteLine("\tcall    GetAnInteger");
                            tw.WriteLine("\tmov ax, [ReadInt]");
                            tw.WriteLine("\tmov [T" + tCounter + "], ax");
                            tw.WriteLine("");
                            break;
                        }
                }

            }
            else
            {
                Console.WriteLine("ISSUUUUUUUUUUUE");
            }
            tw.Close();

        }
        public void equalPop(List<string> pushdown, string prevTerm)
        {
            for (int i = pushdown.Count - 1; i > 0; i--)
            {
                if (pushdown[i] == prevTerm)
                {
                    pushdown.RemoveAt(i);
                    break;
                }
            }
        }

        public void boiler_plate(List<String[]> symArr)
        {
            TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.asm");
            String pgmName = "";
            tw.WriteLine("sys_exit	equ	1");
            tw.WriteLine("sys_read	equ	3");
            tw.WriteLine("sys_write	equ	4");
            tw.WriteLine("stdin		equ	0");
            tw.WriteLine("stdout		equ	1");
            tw.WriteLine("stderr		equ	3\n");

            tw.WriteLine("section .data\t\t;used to declare constants");
            foreach (var array in symArr)
            {
                if (array[1] == "<$PGMNAME>" || int.TryParse(array[0], out _))
                {
                    //pgmName = array[0];
                }
                else
                {
                    tw.WriteLine('\t' + array[0] + "\t db\t" + "'aaaaa'");
                }

            }
            tw.WriteLine("\tuserMsg		db      'Enter an integer(less than 32,765): '");
            tw.WriteLine("\tlenUserMsg	equ	$-userMsg");
            tw.WriteLine("\tnewline		db	0xA 	; 0xA 0xD is ASCII <LF><CR>");

            tw.WriteLine("\tTen             DW      10  ;Used in converting to base ten.");
            tw.WriteLine("\tnum		times 6 db 'ABCDEF'");
            tw.WriteLine("\tnumEnd		equ	$-num");
            tw.WriteLine("\tResult          db      'Ans = '");
            tw.WriteLine("\tResultValue	db	'aaaaa'");
            tw.WriteLine("\t		db	0xA");
            tw.WriteLine("\tResultEnd       equ 	$-Result    ; $=> here, subtract address Result");
            tw.WriteLine("\nsection .bss		;used to declare uninitialized variables\n");
            tw.WriteLine("\tTempChar        RESB    1              ;temp space for use by GetNextChar");
            tw.WriteLine("\ttestchar        RESB    1");
            tw.WriteLine("\tReadInt         RESW    1              ;Temporary storage GetAnInteger.	");
            tw.WriteLine("\ttempint         RESW	1              ;Used in converting to base ten.");

            tw.WriteLine("\tnegflag         RESB    1              ;P=positive, N=negative\n");


            tw.WriteLine("\tglobal _start:\t\t;main program");
            tw.WriteLine("section .text\n");
            tw.WriteLine("_start:");


            tw.Close();
            Console.WriteLine("TBI");
        }

        public void endBoiler(Boolean inputBoiler, Boolean printBoiler)
        {
            TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.asm");

            tw.WriteLine("\nfini:");
            tw.WriteLine("\tmov eax,sys_exit ;terminate, sys_exit = 1");
            tw.WriteLine("\txor ebx,ebx	;successfully, zero in ebx indicates success");
            tw.WriteLine("\tint 80h\n\n");

            tw.WriteLine("PrintString:");
            tw.WriteLine("\tpush    ax");
            tw.WriteLine("\tpush    dx");
            tw.WriteLine("\tmov eax, 4");
            tw.WriteLine("\tmov ebx, 1		; print default output device");
            tw.WriteLine("\tmov ecx, userMsg	; pointer to string");
            tw.WriteLine("\tmov edx, lenUserMsg	; arg1, where to write, screen");
            tw.WriteLine("\tint	80h		; interrupt 80 hex, call kernel");
            tw.WriteLine("\tpop     dx              ;Restore registers.");
            tw.WriteLine("\tpop     ax");
            tw.WriteLine("\tret\n");


            tw.WriteLine("GetAnInteger:	;Get an integer as a string");
            tw.WriteLine("\tmov eax,3	;read");
            tw.WriteLine("\tmov ebx,2	;device");
            tw.WriteLine("\tmov ecx,num	;buffer address");
            tw.WriteLine("\tmov edx,6	;max characters");
            tw.WriteLine("\tint 0x80\n");
            tw.WriteLine("ConvertStringToInteger:");
            tw.WriteLine("\tmov ax,0	;hold integer");
            tw.WriteLine("\tmov [ReadInt],ax ;initialize 16 bit number to zero");
            tw.WriteLine("\tmov ecx,num	;pt - 1st or next digit of number as a string ");
            tw.WriteLine("\tmov bx,0");
            tw.WriteLine("\tmov bl, byte [ecx] ;contains first or next digit");
            tw.WriteLine("Next:	sub bl,'0'	;convert character to number");
            tw.WriteLine("\tmov ax,[ReadInt]");
            tw.WriteLine("\tmov dx,10");
            tw.WriteLine("\tmul dx		;eax = eax * 10");
            tw.WriteLine("\tadd ax,bx");
            tw.WriteLine("\tmov [ReadInt], ax");
            tw.WriteLine("\tmov bx,0");
            tw.WriteLine("\tadd ecx,1 	;pt = pt + 1");
            tw.WriteLine("\tmov bl, byte[ecx]");
            tw.WriteLine("\tcmp bl,0xA	;is it a <lf>");
            tw.WriteLine("\tjne Next	; get next digit");
            tw.WriteLine("\tret\n\n");



            tw.WriteLine("ConvertIntegerToString:");
            tw.WriteLine("\tmov ebx, ResultValue + 4   ;Store the integer as a five");
            tw.WriteLine("ConvertLoop:");
            tw.WriteLine("\tsub dx,dx  ; repeatedly divide dx:ax by 10 to obtain last digit of number");
            tw.WriteLine("\tmov cx,10  ; as the remainder in the DX register.  Quotient in AX.");
            tw.WriteLine("\tdiv cx");
            tw.WriteLine("\tadd dl,'0' ; Add '0' to dl to convert from binary to character.");
            tw.WriteLine("\tmov [ebx], dl");
            tw.WriteLine("\tdec ebx");
            tw.WriteLine("\tcmp ebx,ResultValue");
            tw.WriteLine("\tjge ConvertLoop\n");
            tw.WriteLine("\tret");



            tw.Close();
        }

        public void createPushdown(String[] tokenArr)
        {
            /*
                Program takes in the token array created during the lexical parser, as well as the symbol table csv which was created.
                Items begin being pushed into the pushdown stack upon the program '{' being found. Upon this trigger, every token scanned
                is then checked if found within state table, reserve_words (if/then and while/do should be removed) or single char delimeters.
                If legitimate token is found, and it is not an operator (i.e. a variable or litInt), it is simply pushed into the stack. If it
                *IS* an operator, it is compared to the previous operator to decide if anything needs to be popped. 
            */
            String SymTabPath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\symbol_table.csv";
            StreamReader sr = new StreamReader(SymTabPath);
            var symArr = new List<String[]>();

            //creating array to track if token is a relational operator.
            String DoubOp = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\double_char_delims.txt";
            StreamReader dr = new StreamReader(DoubOp);
            var dOpArr = new List<String>();

            Boolean inputBoiler = false;
            Boolean printBoiler = false;



            Boolean inClass = false;
            int rowsize = 0;
            //holds value of previous found terminal, to more easily determine precedence via operator precidence table.
            string prevTerm = "";
            //holds the resulting precedence from the look up table to determine if a pop needs to happen.
            string precSign;
            int tCount = 0;

            // The following three lines create the operator precedence 2d matrix, by reusing the program
            // which created the original precedence table in the lexical scanner pass. reusability!modularity! p r o f e s s i o n a l g r a d e
            string csvpath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\op_prec.csv";
            statetable opTabCreator = new statetable();
            List<string[]> opPrecTab = opTabCreator.dfsa(csvpath);

            opwords opCreator = new opwords();
            String[] opArr = opCreator.createOpArr();

            List<String> fixup = new List<String>();



            /*****IMPORTANT****
                This is the actual pushdown stack which will hold and pop code appropriately to create the machine language counterpart.
            *****IMPORTANT****/
            List<string> pushdown = new List<string>();
            pushdown.Add(";");


            while (!sr.EndOfStream)
            {
                String[] nextline = sr.ReadLine().Split(',');
                symArr.Add(nextline);
                rowsize++;
            }
            symArr.ToArray();

            //Calling a function to create what I believe to be correct """boiler plate" for the assembly code being produced.
            boiler_plate(symArr);
            rowsize = 0;
            while (!dr.EndOfStream)
            {
                String nextline = dr.ReadLine();
                dOpArr.Add(nextline);
                rowsize++;
            }
            dOpArr.ToArray();


            for (int i = 0; i < tokenArr.GetLength(0); i++)
            {
                Console.WriteLine("array length is: " + tokenArr.GetLength(0));
                Console.WriteLine("i is: " + i);
                if (inClass == false)
                {
                    if (tokenArr[i] is "{")
                    {
                        Console.WriteLine("Now entering program! Inserting " + tokenArr[i] + " into pushdown stack.");
                        inClass = true;
                        prevTerm = tokenArr[i];
                        pushdown.Add(tokenArr[i]);
                    }
                    else
                    {
                        Console.WriteLine("Skipping " + tokenArr[i]);
                    }

                }
                else
                {
                    Boolean pushed = false;
                    //foreach loop to check if the symbol in question is within the symbol table, which would indicate a 'nonterminal'
                    // aka a non-operator (variable, literal integer, etc.)
                    foreach (var array in symArr)
                    {
                        if (array[0] == tokenArr[i])
                        {
                            Console.WriteLine("Found non-terminal " + tokenArr[i] + " within the symbol table. Pushing in stack!");
                            pushdown.Add(tokenArr[i]);
                            pushed = true;
                        }
                        else
                        {
                            //nothing
                        }
                    }

                    //catching edge case of variable being assigned an int with no operator functions involved ( X = 7; for example)
                    if (pushed == false && int.TryParse(tokenArr[i], out _))
                    {
                        Console.WriteLine("Found non-terminal " + tokenArr[i] + " within the symbol table. Pushing in stack!");
                        pushdown.Add(tokenArr[i]);
                        pushed = true;
                    }
                    //now checks, if not in symbol table, if it is an operator/'terminal'. if so, a function is
                    //called to check precedence with last found terminal to determine if pop is needed.
                    if (pushed == false)
                    {
                        //need to implement op/delim array creator function to check.
                        if (opArr.Contains(tokenArr[i]))
                        {
                            Console.WriteLine("Found TERMINAL " + tokenArr[i] + " within operator list. Will look up operator on precedence table...");
                            precSign = findSym(prevTerm, tokenArr[i], opPrecTab);
                            if (precSign == ">")
                            {
                                Console.WriteLine("Implementing pop logic...");
                                if (dOpArr.Contains(prevTerm) && prevTerm != "INPUT")
                                {


                                    prevTerm = relopPop(pushdown, prevTerm);
                                    Console.WriteLine("~~~ FIXUP PRINT ~~~");
                                    fixup.Add("L" + (lCounter - 1));
                                    foreach (var item in fixup)
                                    {
                                        Console.WriteLine(item);
                                    }

                                }
                                else if (prevTerm == "INPUT")
                                {
                                    prevTerm = inputPop(pushdown, prevTerm);

                                }
                                else if (prevTerm == "PRINT")
                                {
                                    prevTerm = printPop(pushdown, prevTerm);
                                }
                                else
                                {
                                    // FUNCTION to pop stack and do stuff. replace earliest *possible* index with a temp (in most cases)
                                    prevTerm = popstack(pushdown, prevTerm); //update prevTerm during this step
                                }

                                if (1 == 1)
                                {
                                    i = i - 1;
                                    Console.WriteLine("retesting item...");
                                    //pushdown.Add(tokenArr[i]);
                                }

                            }
                            else if (precSign == "=")
                            {
                                Console.WriteLine("Special logic needed for two token pop");
                                //pushdown.RemoveAt(pushdown.Count - 1);
                                if (tokenArr[i] == "THEN")
                                {
                                    prevTerm = tokenArr[i];
                                    pushdown.Add(tokenArr[i]);

                                }
                                else if (tokenArr[i] is "}" && fixup.Any())
                                {
                                    equalPop(pushdown, prevTerm);
                                    TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.asm");
                                    tw.WriteLine(fixup.Last() + ":\tnop");
                                    tw.Close();

                                    fixup.RemoveAt(fixup.Count - 1);
                                    pushdown.RemoveAt(pushdown.Count - 1);
                                    pushdown.RemoveAt(pushdown.Count - 1);
                                }
                                else
                                {
                                    equalPop(pushdown, prevTerm);
                                    Console.WriteLine("new pusdhdown after equal pop: ");
                                    foreach (var item in pushdown)
                                    {
                                        Console.WriteLine(item);
                                    }
                                    for (int j = pushdown.Count - 1; j > 0; j--)
                                    {
                                        if (opArr.Contains(pushdown[j]))
                                        {
                                            prevTerm = pushdown[j];
                                            break;
                                        }
                                    }
                                }

                            }
                            else if (precSign == "@")
                            {
                                Console.WriteLine("Throwing away empty semicolon...");
                            }
                            else
                            {
                                Console.WriteLine("operator yields; Placing in stack...");
                                pushdown.Add(tokenArr[i]);
                                //update prevTerm at the end to continue the cycle of easily recalling previous terminal.
                                prevTerm = tokenArr[i];
                            }
                        }
                    }
                    //Console.WriteLine("Pushing " + tokenArr[i] + " into the stack!");
                }
            }
            Console.WriteLine("Possible end??");
            endBoiler(inputBoiler, printBoiler);
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
        }
    }
}