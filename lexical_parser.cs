using System;

/*
THINGS TO WORK ON: Implement Heuristic parser for grabbing characters. 
                    I already have the switch-case implemented, so now it's
                    just about creating the CSV, and creating the state array.
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

            /* For now the path to the test code path is hardcoded, but could later be easily switch to user I/O */
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\PGM1.txt");
            while (pgmreader.Peek() >= 0)
            {
                ch = (char)pgmreader.Read();


                // After ch takes the next pgmreader character (line above), a switch case decides what needs to be done based on what the character is.

                switch (ch)
                {

                    case ' ':
                        {
                            if (tempvar != "")
                            {
                                tokenList.Add(tempvar);
                                tempvar = "";
                                break;
                            }
                            else
                                break;

                        }
                    case '\t':
                        {
                            break;
                        }
                    case '\n':
                        {
                            break;
                        }
                    case ',':
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            tempvar += ch;
                            tokenList.Add(tempvar);
                            tempvar = "";
                            break;
                        }
                    case ';':
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            tempvar += ch;
                            tokenList.Add(tempvar);
                            tempvar = "";
                            break;
                        }
                    case '{':
                        {
                            tokenList.Add(tempvar);
                            tempvar = "";
                            tempvar += ch;
                            tokenList.Add(tempvar);
                            tempvar = "";
                            break;
                        }

                    default:
                        tempvar += ch;
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

            // String[] tokenArr = tokenList();

            // tokenClassifier(tokenArr);

            //statetable bigTest = new statetable();
            //bigTest.dfsa();

            statetable createTable = new statetable();
            createTable.dfsa();

            // foreach (String item in tokenArr)
            // {
            //     Console.WriteLine(item);
            // }
        }

    }

}

