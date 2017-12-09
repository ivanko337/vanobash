using System;
using System.IO;

namespace Bash
{
    class CP : Command
    {
        public override string ProgramName => "cp";

        public override void Execute(params string[] p)
        {
            try
            {
                File.Copy(p[0], p[1]);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
