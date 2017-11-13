using System;
using System.IO;

namespace по_схеме_Сержио
{
    sealed class CD : Command
    {
        public override string ProgramName { get; } = "cd";

        public override void Execute(params string[] p)
        {
            try
            {
                if (p[0] != null)
                {
                    if (Directory.Exists(p[0]))
                    {
                        Program.Path = p[0];
                    }
                    else
                    {
                        Console.WriteLine("vanobash: cd: No such directory.");
                    }
                }
                else
                {
                    Program.Path = @"C:\Users\vano";
                }
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("vanobash: cd: missing operand.");
            }
        }
    }
}
