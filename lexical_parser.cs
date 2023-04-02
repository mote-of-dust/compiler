using System;

/*
THINGS TO WORK ON:  
                    (4-03-2023)
                    Syntax analyzer started. Simple process to scan token array and symbol
                    table can place the correct items into a pushdown stack. CSV of
                    operator precedence table has been made, and logic to implement the
                    check of precedence has been started. The function itself still
                    needs to be implemented, though similar functionality has already
                    been coded with the parse table csv, which can be partially used
                    for this purpose as well.

                    (2-25-2023)
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
                // Console.WriteLine("Next char read is: " + ch.ToString());

                // Console.WriteLine("Is " + ch + " a letter: ");
                // Console.WriteLine(Char.IsLetter(ch));


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
                        // Console.WriteLine("Next state for char " + ch + " is " + nextstate.ToString());
                        //Console.WriteLine("Next state is: " + statetab[curstate + 1][i]);
                        break;
                    }
                    else
                    {
                        // Console.WriteLine(statetab[0][staterow]);
                        // Console.WriteLine("False");
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


                            // //checking if the var is touching a + , * / etc.

                            // // if (ch == ' ')
                            // // {
                            // //     tempvar = "";
                            // // }
                            // // else
                            // // {
                            // //     tempvar = ch.ToString();
                            // // }
                            // // curstate = Convert.ToInt32(statetab[1][staterow]);
                            // tempvar = "";
                            // curstate = 0;
                            // //Console.WriteLine("Next state")
                            // break;

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
                            // tempvar = "";
                            // Console.WriteLine("before: " + tempvar);
                            // if (nextstate == 0)
                            // {
                            //     tempvar = "";
                            // }
                            // Console.WriteLine("After: " + tempvar);
                            // Console.WriteLine(nextstate);
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

                // catches edge case of last character to be read, and correctly types it.
                // if (!(pgmreader.Peek() > -1))
                // {
                //     //Console.WriteLine("Current state = " + curstate);
                //     //Console.WriteLine(statetab[curstate + 1][7]);
                //     int tempstate = Convert.ToInt32(statetab[curstate + 1][7]);
                //     tokenList.Add(tempvar + " " + statetab[tempstate + 1][1]);
                //     //Console.WriteLine("test " + statetab[tempstate + 1][1]);
                // }
            }

            tokenvars = tokenList.ToArray();
            pgmreader.Close();


            return tokenvars;
        }

        static void Main(String[] args)
        {
            String filePath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\PGM1.txt";

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

            syntaxAnalyzer pushdown = new syntaxAnalyzer();
            pushdown.createPushdown(tokenArr);
        }

    }

}

