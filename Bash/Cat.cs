using System;
using System.IO;

namespace Bash
{
    sealed class Cat : Command
    {
        public override string ProgramName => "cat";

        public override void Execute(params string[] p)
        {
            try
            {
                string path = ServiceClass.GetOnlyDirectory(p[0]);
                if (Directory.Exists(path))
                {
                    PrintFile(path);
                }
                else
                {
                    PrintFile(Program.Path + "\\" + p[0]);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("vanobash: cat: File not found");
            }
        }

        private void PrintFile(string path)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string line = sr.ReadLine();
                while(line != null)
                {
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
            }
        }
    }
}
