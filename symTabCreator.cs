using System;

namespace app
{
    class symTabCreator
    {
        public void createTable(String[] tokenArr)
        {
            var csvpath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\tokens.csv";
            StreamReader sr = new StreamReader(csvpath);
            Boolean csSeg = false;
            int csAddr = 0;
            int dsAddr = 0;
            List<String> added_symbols = new List<string>();

            var tokenlist = new List<String[]>();
            int rowsize = 0;
            int nextstate = 1;

            reservewords resChecker = new reservewords();
            String[] resCheck = resChecker.createResArr();

            while (!sr.EndOfStream)
            {
                String[] nextline = sr.ReadLine().Split('|');
                tokenlist.Add(nextline);
                rowsize++;
            }


            using StreamWriter file = new("symbol_table.csv");
            for (int i = 0; i < tokenArr.GetLength(0); i++)
            {
                if (resCheck.Contains(tokenlist[i][0]) || (tokenlist[i][1] == "<$litInt>" && tokenlist[i - 1][0] == "=") || added_symbols.Contains(tokenlist[i][0]))
                {
                    ;
                }
                else if (tokenlist[i][1] == "<$PGMNAME>" || tokenlist[i][1] == "<$PROCNAME>")
                {
                    file.WriteLineAsync(tokenlist[i][0] + "," + tokenlist[i][1] + "," + "," + csAddr + "," + "CS");
                    added_symbols.Add(tokenlist[i][0]);
                    csAddr++;
                }
                else if (int.TryParse(tokenlist[i][0], out _) && tokenlist[i - 1][0] != "=")
                {
                    file.WriteLineAsync(tokenlist[i][0] + "," + tokenlist[i][1] + "," + tokenlist[i][0] + "," + dsAddr + "," + "DS");

                    dsAddr += 2;
                }
                else if (tokenlist[i + 1][0] == "=")
                {
                    String temp = "";
                    int tempIndex = i + 2;

                    while (tokenlist[tempIndex][0] != "," && tokenlist[tempIndex][0] != ";")
                    {
                        temp += tokenlist[tempIndex][0];
                        tempIndex++;
                    }
                    Console.WriteLine("Possible value of " + tokenlist[i][0] + " is: " + temp);
                    if (int.TryParse(temp, out _))
                    {
                        file.WriteLineAsync(tokenlist[i][0] + "," + tokenlist[i][1] + "," + temp + "," + dsAddr + "," + "DS");
                        dsAddr += 2;
                        added_symbols.Add(tokenlist[i][0]);
                    }
                    else
                    {
                        file.WriteLineAsync(tokenlist[i][0] + "," + tokenlist[i][1] + "," + "?" + "," + dsAddr + "," + "DS");
                        dsAddr += 2;
                        added_symbols.Add(tokenlist[i][0]);
                    }
                }
                else
                {
                    file.WriteLineAsync(tokenlist[i][0] + "," + tokenlist[i][1] + "," + "?" + "," + dsAddr + "," + "DS");
                    dsAddr += 2;
                    added_symbols.Add(tokenlist[i][0]);
                }


            }
            string tempo = "T";
            // lastly, adding 5 temporary variables
            for (int i = 1; i < 11; i++)
            {
                file.WriteLineAsync((tempo + i.ToString()) + "," + "<var>" + "," + "?" + "," + dsAddr + "," + "DS");
                dsAddr += 2;
            }
        }
    }
}