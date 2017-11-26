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
                    ChangeDirectory(p[0]);
                else if (p[0] == null)
                    ChangeDirectory(@"C:\Users\vano");
                else
                    Console.WriteLine("vanobash: cd: No such directory.");
            }
            catch (IndexOutOfRangeException)
            {
                Program.Path = @"C:\Users\vano";
            }
        }

        public static void ChangeDirectory(string path)
        {
            Program.Path = path;
            string temppath = "";
            if (path == Program.HomeDirectory)
                temppath = "~";
            else
                temppath = path;
            Console.Title = @"vanobash - " + Program.UserName + "@" + Program.CompName + ": " + temppath;
        }
    }
}
