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
                string path = ServiceClass.GetOnlyDirectory(p[0]);
                if (p[0] == "..\\" || p[0] == ".." || p[0] == "../")
                {
                    string temppath = ServiceClass.GetOnlyDirectory(Program.Path);
                    Program.OldPath = Program.Path;
                    ChangeDirectory(temppath);
                }
                else if (Directory.Exists(path))
                {
                    Program.OldPath = Program.Path;
                    ChangeDirectory(p[0]);
                }
                else if (p[0] == null)
                {
                    Program.OldPath = Program.Path;
                    ChangeDirectory(@"C:\Users\vano");
                }
                // Это костыль! Не трогать. Никак иначе не работает
                else if (p[0] == "-")
                {
                    string tempOldDir = Program.Path;
                    ChangeDirectory(Program.OldPath);
                    Program.OldPath = tempOldDir;
                }
                else if(p[0] == "")
                {
                    Program.OldPath = Program.Path;
                    Program.Path = @"C:\Users\vano";
                }
                else if(Directory.Exists(Program.Path + "\\" + p[0]))
                {
                    Program.OldPath = Program.Path;
                    ChangeDirectory(Program.Path + "\\" + p[0]);
                }
                else
                    Console.WriteLine("vanobash: cd: No such directory.");
            }
            catch (IndexOutOfRangeException)
            {
                Program.OldPath = Program.Path;
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
        
        private void execute(params string[] p)
        {

        }
    }
}
