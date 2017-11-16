using System;
using System.IO;

namespace Bash
{
    sealed class UserAdd : Command
    {
        public override string ProgramName => "useradd";

        public override void Execute(params string[] p)
        {
            if (p[0] != null && p[1] != null)
                Write(p[0], p[1]);
            else if(p[0] == null)
                Console.WriteLine("vanobash: useradd: User name is not specified");
            else if(p[1] == null)
                Console.WriteLine("vanobash: useradd: Password is not specified");
            else
                Console.WriteLine("vanobash: useradd: User name and password is not specified");
        }

        static void Write(string user, string pass)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\Program Files\Vanobash\pass.txt", true))
            { sw.WriteLine(Login.GetHash(user + ":" + pass)); }
        }
    }
}
