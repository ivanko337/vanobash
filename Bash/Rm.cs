using System;
using System.IO;

namespace Bash
{
    class Rm : Command
    {
        public override string ProgramName => "rm";

        public override void Execute(params string[] p)
        {
            try
            {
                if (p[0] != null)
                    RemoveFile(p[0]);
                else
                    Console.WriteLine("vanobash: rm: File not exist");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void RemoveFile(string path)
        {
            if (Directory.Exists(ServiceClass.GetOnlyDirectory(path)) && File.Exists(path))
                File.Delete(path);
            else if (File.Exists(Program.Path + "\\" + path))
                File.Delete(Program.Path + "\\" + path);
            else
                Console.WriteLine("vanobash: rm: File not exist");
        }
    }
}
