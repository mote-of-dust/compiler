using System;

namespace app
{
    class statetable
    {
        public List<String[]> dfsa()
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

            return statelist;
        }
    }
}