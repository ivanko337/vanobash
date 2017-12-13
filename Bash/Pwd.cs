using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bash
{
    sealed class Pwd : Command
    {
        public override string ProgramName => "pwd";

        public override void Execute(params string[] p)
        {
            Console.WriteLine(Program.Path);
        }
    }
}
