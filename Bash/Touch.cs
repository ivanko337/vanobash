using System;
using System.IO;

namespace по_схеме_Сержио
{
    sealed class Touch : Command
    {
        public override string ProgramName => "touch";

        public override void Execute(params string[] p)
        {
            try
            {
                string path = CutPath(p[0]);
                if (Directory.Exists(path))
                {
                    File.Create(path);
                }
                else
                {
                    string filePath = Program.Path + p[0];
                    File.Create(filePath);
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("vanobash: touch: missing operand.");
            }
        }

        private string CutPath(string path)
        {
            string answer = "";

            for (int i = path.Length; i > 0; i--)
            {
                answer += path[i];
                if (path[i - 1] == '\\' || path[i - 1] == '/')
                {
                    break;
                }
            }

            return answer;
        }

        private static bool isEven(int n)
        {
            for(int i = 0; i <= n; i++)
            {
                if (i == n && isEven(i))
                    return true;
            }
            return false;
        }
    }
}
