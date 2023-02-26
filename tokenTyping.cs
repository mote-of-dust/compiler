using System;
using System.IO;
using System.Text;

namespace app
{
    class tokenTyping
    {
        public async void createTokenCsv(String[] tokenArr)
        {
            var csvpath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\special_chars.csv";
            StreamReader sr = new StreamReader(csvpath);
            var statelist = new List<String[]>();
            int rowsize = 0;
            int nextstate = 1;
            String filename = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\tokens.csv";
            String ch = "";
            Boolean classCheck = false;
            Boolean procCheck = false;
            Boolean constCheck = false;
            Boolean isNumeric = false;

            while (!sr.EndOfStream)
            {
                String[] nextline = sr.ReadLine().Split('|');
                statelist.Add(nextline);
                rowsize++;
            }
            statelist.ToArray();


            using StreamWriter file = new("tokens.csv");
            for (int i = 0; i < tokenArr.GetLength(0); i++)
            {
                Console.WriteLine("current token is: " + tokenArr[i]);
                ch = tokenArr[i];
                isNumeric = int.TryParse(ch, out _);

                int temp = Array.IndexOf(statelist[0], ch);
                Console.WriteLine("index found at: " + temp);
                if (temp != -1)
                {
                    await file.WriteLineAsync(tokenArr[i] + "|" + statelist[1][temp]);
                    if (statelist[1][temp] == "<$CLASS>")
                    {
                        classCheck = true;
                    }
                    else if (statelist[1][temp] == "<$PROCEDURE>")
                    {
                        procCheck = true;
                    }
                    else if (statelist[1][temp] == "<$CONST>")
                    {
                        constCheck = true;
                    }
                    else if (statelist[1][temp] == "<semi>")
                    {
                        Console.WriteLine("const should end now.");
                        procCheck = false;
                        constCheck = false;
                    }
                }
                else
                {
                    if (isNumeric)
                    {
                        await file.WriteLineAsync(tokenArr[i] + "|" + "<$litInt>");
                        isNumeric = false;
                    }
                    else if (classCheck)
                    {
                        await file.WriteLineAsync(tokenArr[i] + "|" + "<$PGMNAME>");
                        classCheck = false;
                    }
                    else if (procCheck)
                    {
                        await file.WriteLineAsync(tokenArr[i] + "|" + "<$PROCNAME>");
                        procCheck = false;
                    }
                    else if (constCheck)
                    {
                        await file.WriteLineAsync(tokenArr[i] + "|" + "<$CONSTVAR>");
                    }
                    else
                    {
                        await file.WriteLineAsync(tokenArr[i] + "|" + "<var>");
                    }
                }
            }


        }
    }
}