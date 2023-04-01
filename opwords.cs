using System;

namespace app
{
    class opwords
    {
        public String[] createOpArr()
        {
            String filepath = @"F:\Documents\SHSU\SHSU Spring 2023\Compiler Design\compiler_files\app\operator_list.txt";
            StreamReader sr = new StreamReader(filepath);
            String line = "";
            List<String> opList = new List<String>();

            while ((line = sr.ReadLine()) != null)
            {
                opList.Add(line);
            }
            sr.Close();

            String[] opArr = opList.ToArray();

            return opArr;


        }
    }
}