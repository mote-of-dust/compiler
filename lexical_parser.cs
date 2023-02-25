using System;

/*
THINGS TO WORK ON:  (2-25-2023)
                    class completed which takes in the fsa state csv and creates
                    a List<String[]> variable out of it. Switch case statements have
                    begun to be adjusted to make it's decisions based off the csv,
                    current state, and the current tempvar. Propper typing for each
                    token appears to be working (for what is currently implemented)
                    Rest of the switch cases Need to be completed,after which all that 
                    is left is completing the symbol table based off of the token-list.






                    (2- 22-2023)
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
            int nextstate = 0;
            int staterow = 0;


            statetable createTable = new statetable();
            List<String[]> statetab = createTable.dfsa();

            // for (int i = 0; i < statetab[0].GetLength(0); i++)
            // {
            //     String[] temp = statetab[i];
            //     for (int j = 0; j < temp.GetLength(0); j++)
            //     {
            //         Console.Write(statetab[i][j] + ' ');
            //     }
            //     Console.Write('\n');
            // }



            /* For now the path to the test code path is hardcoded, but could later be easily switch to user I/O */
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\PGM2.txt");
            while (pgmreader.Peek() > -1)
            {
                ch = (char)pgmreader.Read();

                Console.WriteLine("Is " + ch + " a letter: ");
                Console.WriteLine(Char.IsLetter(ch));


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


                for (staterow = 1; staterow < statetab[0].GetLength(0); staterow++)
                {
                    if (statetab[0][staterow] == chSym.ToString())
                    {
                        nextstate = Convert.ToInt32(statetab[curstate + 1][staterow]);
                        Console.WriteLine("Next state for char " + ch + " is " + nextstate.ToString());
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
                                tempvar = ch.ToString() + " <mop>";
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                            else
                            {
                                tokenList.Add(tempvar);
                                tempvar = ch.ToString() + " <mop>";
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
                    case 6:
                        {
                            reservewords ResObj = new reservewords();
                            String[] ResArr = ResObj.createResArr();
                            if (Array.IndexOf(ResArr, tempvar) >= 0)
                            {
                                tokenList.Add(tempvar + " <$" + ResArr[Array.IndexOf(ResArr, tempvar)] + ">");
                            }
                            else
                            {
                                tokenList.Add(tempvar + " " + statetab[nextstate + 1][1]);
                            }

                            if (ch == ' ')
                            {
                                tempvar = "";
                            }
                            else
                            {
                                tempvar = ch.ToString();
                            }
                            curstate = Convert.ToInt32(statetab[1][staterow]);
                            //Console.WriteLine("Next state")
                            break;

                        }
                    case 11:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }
                    case 12:
                        {
                            tempvar += " " + statetab[nextstate + 1][1];
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }

                    default:
                        break;

                }

                // catches edge case of last character to be read, and correctly types it.
                if (!(pgmreader.Peek() > -1))
                {
                    //Console.WriteLine("Current state = " + curstate);
                    //Console.WriteLine(statetab[curstate + 1][7]);
                    int tempstate = Convert.ToInt32(statetab[curstate + 1][7]);
                    tokenList.Add(tempvar + " " + statetab[tempstate + 1][1]);
                    //Console.WriteLine("test " + statetab[tempstate + 1][1]);
                }
            }

            tokenvars = tokenList.ToArray();
            pgmreader.Close();


            return tokenvars;
        }

        static void Main(String[] args)
        {

            String[] tokenArr = tokenList();

            Console.WriteLine("-----------------------");

            for (int i = 0; i < tokenArr.GetLength(0); i++)
            {
                Console.WriteLine(tokenArr[i]);
            }
        }

    }

}

