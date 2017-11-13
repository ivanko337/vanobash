using System;
using System.IO;

namespace по_схеме_Сержио
{
    sealed class Mkdir : Command
    {
        public override string ProgramName => "mkdir";

        public override void Execute(params string[] p)
        {
            try
            {
                if (p[0] == null)
                {
                    Console.WriteLine("vanobash: mkdir: missing operand.");
                }
                else
                {
                    Directory.CreateDirectory(p[0]);
                }
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("vanobash: mkdir: missing operand.");
            }
        }

        private bool EstLiPapka(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            return dir.Exists;
        }
    }
}
