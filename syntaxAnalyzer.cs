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

        // popstack() will call this function once the popped list<string> is created. This function will append a file to
        public void mlWriter(List<string> popped)
        {
            TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.txt");
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

                            tw.WriteLine("\tmove [T" + tCounter + "], ax\n");
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

                            tw.WriteLine("\tmove [T" + tCounter + "], ax\n");
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
                                tw.WriteLine("\tmul ax, " + popped[2]);
                            }
                            else
                            {
                                tw.WriteLine("\tmul ax, [" + popped[2] + "]");
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
            TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.txt");
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
                if (array[1] == "<$PGMNAME>")
                {
                    pgmName = array[0];
                }
                else
                {
                    tw.WriteLine('\t' + array[0] + "\t db\t" + array[2]);
                }

            }
            tw.WriteLine("\nsection .bss		;used to declare uninitialized variables\n");
            tw.WriteLine("\tTempChar        RESB    1              ;temp space for use by GetNextChar");
            tw.WriteLine("\ttestchar        RESB    1");
            tw.WriteLine("\tReadInt         RESW    1              ;Temporary storage GetAnInteger.	");
            tw.WriteLine("\ttempint         RESW	1              ;Used in converting to base ten.");
            tw.WriteLine("\tnegflag         RESB    1              ;P=positive, N=negative\n");
            tw.WriteLine("\tglobal " + pgmName + "\t\t;main program");
            tw.WriteLine("section .text\n");
            tw.WriteLine(pgmName + ":");


            tw.Close();
            Console.WriteLine("TBI");
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

            // foreach (var item in dOpArr)
            // {
            //     Console.WriteLine(item);
            // }

            //double checking everything was written correctly to list<string[]>
            // foreach (var array in symArr)
            // {
            //     // test case just to get index 0// Console.Write(array[0]);
            //     // foreach (var item in array)
            //     // {
            //     //     Console.Write(item + " ");

            //     // }
            //     Console.Write('\n');
            // }

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
                                if (dOpArr.Contains(prevTerm))
                                {
                                    // Console.WriteLine("Special code needed for relational operator!");
                                    // Console.WriteLine(">>> SHUTTING DOWN...");

                                    // // Console app
                                    // System.Environment.Exit(1);

                                    prevTerm = relopPop(pushdown, prevTerm);
                                    Console.WriteLine("~~~ FIXUP PRINT ~~~");
                                    fixup.Add("L" + (lCounter - 1));
                                    foreach (var item in fixup)
                                    {
                                        Console.WriteLine(item);
                                    }

                                }
                                else
                                {
                                    // FUNCTION to pop stack and do stuff. replace earliest *possible* index with a temp (in most cases)
                                    prevTerm = popstack(pushdown, prevTerm); //update prevTerm during this step
                                }

                                if (tokenArr[i] != ";")
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
                                    TextWriter tw = File.AppendText(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.txt");
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
                                Console.WriteLine("special code to be implemented for this pop.");
                                // special case logic/pop for a declaration.
                                pushdown.Add(tokenArr[i]);
                                //update prevTerm at the end to continue the cycle of easily recalling previous terminal.
                                prevTerm = tokenArr[i];
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
            foreach (var item in pushdown)
            {
                Console.WriteLine(item);
            }
        }
    }
}