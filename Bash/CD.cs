using System;
using System.IO;

namespace Bash
{
    sealed class CD : Command
    {
        public override string ProgramName { get; } = "cd";

        public override void Execute(params string[] p)
        {
            try
            {
                if (Directory.Exists(p[0]))
                    Program.Path = p[0];
                else if(p[0] == null)
                    Program.Path = @"C:\Users\vano";
                else
                    Console.WriteLine("vanobash: cd: No such directory.");
            }
            catch (IndexOutOfRangeException)
            {
                Program.Path = @"C:\Users\vano";
            }
        }
    }
}
