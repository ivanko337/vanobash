using System;
using System.IO;

namespace по_схеме_Сержио
{
    sealed class LS : Command
    {
        public override string ProgramName { get; } = "ls";

        public override void Execute(params string[] p)
        {
            DirectoryInfo dir = new DirectoryInfo(Program.Path);
            foreach (var item in dir.GetFiles())
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(item.Name);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
