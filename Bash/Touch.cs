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
                    string filePath = Program.Path + "\\" + p[0];
                    File.Create(filePath);
                }
            }
            catch (Exception ex) //IndexOutOfRange
            {
                //Console.WriteLine("vanobash: touch: missing operand.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
