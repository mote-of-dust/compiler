using System;

namespace app
{
    class syntaxAnalyzer
    {
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

            opwords opCreator = new opwords();
            String[] opArr = opCreator.createOpArr();

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
                    }
                    else
                    {
                        Console.WriteLine("Skipping " + tokenArr[i]);
                    }

                }
                else
                {
                    Boolean pushed = false;
                    foreach (var array in symArr)
                    {
                        if (array[0] == tokenArr[i])
                        {
                            Console.WriteLine("Found non-terminal " + tokenArr[i] + " within the symbol table. Pushing in stack!");
                            pushed = true;
                        }
                        else
                        {
                            // Console.WriteLine("array[0] = " + array[0]);
                            // Console.WriteLine("tokenArr[i] = " + tokenArr[i]);
                        }
                    }
                    if (pushed == false)
                    {
                        //need to implement op/delim array creator function to check.
                        if (opArr.Contains(tokenArr[i]))
                        {
                            Console.WriteLine("Found TERMINAL " + tokenArr[i] + " within operator list. Will look up operator on precedence table...");

                        }

                    }
                    //Console.WriteLine("Pushing " + tokenArr[i] + " into the stack!");
                }

            }
        }
    }
}