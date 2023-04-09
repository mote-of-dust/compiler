using System;

namespace app
{

    class syntaxAnalyzer
    {
        public static int tCounter = 0;

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

            //FUNCTION TO CALL SWITCHCASE AND MAKE ML CODE

            Console.WriteLine("PSA after pop: ");
            if (prevTerm != "=")
            {
                tCounter++;
                pushdown.Add(tempo + tCounter.ToString());
                foreach (var item in pushdown)
                {
                    Console.WriteLine(item);
                }
                return pushdown[(pushdown.Count) - 2];
            }
            else
            {
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
            // create a switch case based on item at index 1 (middle index)
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
                                // FUNCTION to pop stack and do stuff. replace earliest *possible* index with a temp (in most cases)
                                prevTerm = popstack(pushdown, prevTerm); //update prevTerm during this step
                                if (tokenArr[i] != ";")
                                {
                                    pushdown.Add(tokenArr[i]);
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