using System;
using System.IO;

namespace Bash
{
    sealed class Cat : Command
    {
        public override string ProgramName => "cat";

        public override void Execute(params string[] p)
        {
            if(p[0] == null)
                Console.WriteLine("vanobash: cat: File not found");
            else
            {
                string file = File.ReadAllText(p[0]);
                Console.WriteLine(file);
            }
        }
    }
}
