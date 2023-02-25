using System;

/*
THINGS TO WORK ON: (2- 22-2023)
                    CSV made, and code written to read it and put in list.
                    Next will be working out the switch case, and potential nested
                    looping needed to parse code and traverse the FSA table. 
                    Examples to look at are: pg 48 of the hymnal,
                    pg 50, and 51 as well.
 */
namespace app
{
    class lexical_parser
    {
        static String[] tokenList()
        {
            // temp str variable to hold reserved words as well as variable names and literal integers
            String tempvar = "";
            // temporary list to hold tokens. List structure was used as it is dynamic, to accomodate a input text of size n.
            List<String> tokenList = new List<String>();
            // the return array which will be futher analyzed in the next method.
            String[] tokenvars;
            // temp char variable to hold the character the file reader is reading.
            char ch;
            char chSym;
            int curstate = 0;
            int nextstate;


            statetable createTable = new statetable();
            List<String[]> statetab = createTable.dfsa();

            for (int i = 0; i < statetab[0].GetLength(0); i++)
            {
                String[] temp = statetab[i];
                for (int j = 0; j < temp.GetLength(0); j++)
                {
                    Console.Write(statetab[i][j] + ' ');
                }
                Console.Write('\n');
            }



            /* For now the path to the test code path is hardcoded, but could later be easily switch to user I/O */
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\PGM1.txt");
            while (pgmreader.Peek() >= 0)
            {
                ch = (char)pgmreader.Read();

                if (Char.IsLetter(ch))
                {
                    chSym = 'L';
                }
                else if (Char.IsDigit(ch))
                {
                    chSym = 'D';
                }
                else if (ch == '\t' || ch == '\n')
                {
                    chSym = ' ';
                }
                else
                {
                    chSym = ch;
                }

                for (int i = 0; i < statetab[0].GetLength(0); i++)
                {
                    if (statetab[0][i] == chSym.ToString())
                    {
                        nextstate = Convert.ToInt32(statetab[curstate + 1][i]);
                        //Console.WriteLine("Next state is: " + statetab[curstate + 1][i]);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("False");
                    }
                }


                // After ch takes the next pgmreader character (line above), a switch case decides what needs to be done based on what the character is.

                switch (nextstate)
                {

                    case 0:
                        {
                            if (tempvar != "")
                            {
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = nextstate;
                                break;
                            }
                            else
                                curstate = nextstate;
                            break;

                        }

                    case 2:
                        {
                            if (curstate == 0)
                            {
                                tempvar = ch.ToString() + "<mop>";
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                            else
                            {
                                tokenList.Add(tempvar);
                                tempvar = ch.ToString() + "<mop>";
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                        }

                    case 3:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }
                    case 4:
                        {
                            tempvar += "<int>";
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }

                    case 5:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }

                    default:
                        break;

                }
            }
            tokenList.Add(tempvar);
            tokenvars = tokenList.ToArray();
            pgmreader.Close();


            return tokenvars;
        }

        static void tokenClassifier(String[] tokenArr)
        {
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\reserved_words.txt");
            String line;
            //List  to hold reserved words
            List<String> reserved = new List<string>();

            while ((line = pgmreader.ReadLine()) != null)
            {
                reserved.Add(line);
            }
            Console.WriteLine(reserved.Contains("Big Yoshi"));
            foreach (String item in reserved)
            {
                Console.WriteLine(item);
            }
            pgmreader.Close();
            //pgmreader = new StreamReader ()

        }
        static void Main(String[] args)
        {

            String[] tokenArr = tokenList();



            // tokenClassifier(tokenArr);

            //statetable bigTest = new statetable();
            //bigTest.dfsa();

            // foreach (String item in tokenArr)
            // {
            //     Console.WriteLine(item);
            // }
        }

    }

}

