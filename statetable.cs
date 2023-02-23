using System;

namespace app
{
    class statetable
    {
        public void dfsa()
        {


            var csvpath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\marc_parse_table.csv";
            StreamReader sr = new StreamReader(csvpath);
            var statelist = new List<String[]>();
            int rowsize = 0;
            int nextstate = 1;

            while (!sr.EndOfStream)
            {
                String[] nextline = sr.ReadLine().Split(',');
                statelist.Add(nextline);
                rowsize++;
            }

            foreach (String[] item in statelist)
            {
                foreach (String token in item)
                {
                    Console.Write(token);
                }
                Console.Write("\n");
            }

            //Console.WriteLine(statelist[1][17]);


            /*
            This will be the overall format of the nested loop needed
            to parse the parse table and find what state the FSA transitions to next

            Start state 0 is thus offset by one due to top row, and every state after that
            is also +1; so at state 0 if you get a letter, the fsa says to go to state 5,
            you then know when you call statelist, it will be statelist[5+1] => statelist[6]
             */
            for (int i = 0; i < statelist[0].GetLength(0); i++)
            {
                String[] temp = statelist[i];
                for (int j = 0; j < temp.GetLength(0); j++)
                {
                    Console.Write(statelist[i][j] + ' ');
                }
                Console.Write('\n');
            }

        }
    }
}