using System;

namespace app
{
    class reservewords
    {
        public String[] createResArr()
        {
            StreamReader pgmreader = new StreamReader(@"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\reserved_words.txt");
            String line;
            //List  to hold reserved words
            List<String> reserved = new List<string>();

            while ((line = pgmreader.ReadLine()) != null)
            {
                reserved.Add(line);
            }
            // Console.WriteLine(reserved.Contains("Big Yoshi"));
            // foreach (String item in reserved)
            // {
            //     Console.WriteLine(item);
            // }
            pgmreader.Close();

            String[] resArr = reserved.ToArray();

            // for (int i = 0; i < resArr.GetLength(0); i++)
            // {
            //     Console.WriteLine(resArr[i]);
            // }
            //pgmreader = new StreamReader ()

            return resArr;
        }
    }
}