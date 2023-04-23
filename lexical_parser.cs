using System;


namespace app
{
    class lexical_parser
    {
        static String[] tokenList(String filePath)
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
            /*curstate is a variable that holds the current state you are in via the previous element in streamreader.
              next state is found by using curstate as a column+1 index. */
            int curstate = 0;

            /* next state is the state found via the state table based on your current state, and the symbol found via streamreader. */
            int nextstate = 0;
            int staterow = 0;


            // The following three lines creates the precedence table needed for the state machine to work.
            statetable createTable = new statetable();
            string csvpath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\marc_parse_table.csv";
            List<String[]> statetab = createTable.dfsa(csvpath);




            StreamReader pgmreader = new StreamReader(filePath);
            while (pgmreader.Peek() > -1)
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
                else if (Char.IsWhiteSpace(ch) || ch == '\n')
                {
                    chSym = ' ';
                }
                else if (ch == ',')
                {
                    chSym = '|';
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

                        break;
                    }
                    else
                    {
                    }
                }


                // After ch takes the next pgmreader character (line above), a switch case decides what needs to be done based on what the character is.

                switch (nextstate)
                {

                    case 0:
                        {
                            if (!(String.IsNullOrWhiteSpace(tempvar)))
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
                                tempvar = ch.ToString();
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                            else
                            {
                                tokenList.Add(tempvar);
                                tempvar = ch.ToString();
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
                            tokenList.Add(tempvar);
                            if (Char.IsWhiteSpace(ch))
                            {
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                            else
                            {
                                tempvar = ch.ToString();
                                curstate = 0;
                                break;
                            }
                        }

                    case 5:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }
                    case 6:
                        {

                            tokenList.Add(tempvar);
                            if (Char.IsWhiteSpace(ch))
                            {
                                tempvar = "";
                                curstate = 0;
                                break;
                            }
                            else
                            {
                                tempvar = ch.ToString();
                                tokenList.Add(tempvar);
                                tempvar = "";
                                curstate = 0;
                                break;
                            }

                        }
                    case 7:
                        {
                            tempvar += ch.ToString();
                            curstate = nextstate;
                            break;
                        }
                    case 8:
                        {
                            tempvar = "";
                            curstate = nextstate;
                            break;
                        }
                    case 9:
                        {

                            curstate = nextstate;
                            break;
                        }
                    case 10:
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
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
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 13:
                        {
                            tempvar += ch;
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 14:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }
                    case 15:
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 16:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            curstate = 0;
                            break;
                        }
                    case 17:
                        {
                            tempvar = ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 18:
                        {
                            tempvar = ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 19:
                        {
                            tempvar = ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 20:
                        {
                            tempvar = ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 21:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 22:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 23:
                        {
                            tempvar += ch.ToString();
                            curstate = nextstate;
                            break;
                        }
                    case 24:
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 25:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 26:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 27:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }
                    case 28:
                        {
                            tempvar += ch;
                            curstate = nextstate;
                            break;
                        }
                    case 29:
                        {
                            tempvar += ch.ToString();
                            tokenList.Add(tempvar);
                            tempvar = "";
                            curstate = 0;
                            break;
                        }

                    default:
                        Console.WriteLine("Error!");
                        curstate = 0;
                        break;

                }

            }

            tokenvars = tokenList.ToArray();
            pgmreader.Close();


            return tokenvars;
        }

        static void Main(String[] args)
        {
            String filePath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\PGM5.txt";

            String[] tokenArr = tokenList(filePath);

            Console.WriteLine("-----------------------");

            for (int i = 0; i < tokenArr.GetLength(0); i++)
            {
                Console.WriteLine(tokenArr[i]);
            }

            tokenTyping typer = new tokenTyping();
            typer.createTokenCsv(tokenArr);

            symTabCreator newTab = new symTabCreator();
            newTab.createTable(tokenArr);

            Console.WriteLine("~~~~~Lexical portion finished~~~~~");

            var MLcreator = File.Create(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\ML.asm");
            MLcreator.Close();

            syntaxAnalyzer pushdown = new syntaxAnalyzer();
            pushdown.createPushdown(tokenArr);
        }

    }

}

