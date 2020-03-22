using System;
using System.Collections.Generic;
using System.IO;

namespace Bash
{
    sealed class LS : Command
    {
        public override string ProgramName { get; } = "ls";

        public override void Execute(params string[] p)
        {
            DirectoryInfo dir = new DirectoryInfo(Program.Path);
            List<string> list = new List<string>();

            foreach (var item in dir.GetFiles())
                list.Add('f' + item.Name);

            foreach (var item in dir.GetDirectories())
                list.Add('d' + item.Name);
            list.Sort();
            int i = 0;
            foreach (var item in list)
            {
                // Определяет каким цветом печатать данную строку
                if (item[0] == 'd')
                {
                    var consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    PrintWord(item);
                    Console.ForegroundColor = consoleColor;
                }
                else
                {
                    var consoleColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    PrintWord(item);
                    Console.ForegroundColor = consoleColor;
                }
                Console.WriteLine();
                i++;
            }
        }

        private void PrintWord(string item)
        {
            for (int i = 1; i < item.Length; i++)
            {
                Console.Write(item[i]);
            }
        }
    }
}
