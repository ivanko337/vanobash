using System;
using System.IO;

namespace Bash
{
    sealed class Touch : Command
    {
        public override string ProgramName => "touch";

        public override void Execute(params string[] p)
        {
            try
            {
                string path = ServiceClass.GetOnlyDirectory(p[0]);
                if (Directory.Exists(path))
                {
                    File.Create(path);
                }
                else
                {
                    File.Create(Program.Path + "\\" + p[0]);
                }
            }
            catch (IndexOutOfRangeException) 
            {
                try
                {
                    File.Create(Program.Path + "Unnamed.txt");
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
