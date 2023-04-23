using System;

namespace app
{
    class reservewords
    {
        public String[] createResArr()
        {
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\special_chars.txt");
            String line;
            //List  to hold reserved words
            List<String> reserved = new List<string>();

            while ((line = pgmreader.ReadLine()) != null)
            {
                reserved.Add(line);
            }
            pgmreader.Close();

            String[] resArr = reserved.ToArray();

            return resArr;
        }
    }
}