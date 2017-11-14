using System;
using System.Collections.Generic;

namespace Bash
{
    class Program
    {
        /// <summary>
        /// Список команд
        /// </summary>
        static List<Command> commands = new List<Command>();

        /// <summary>
        /// Директория
        /// </summary>
        public static string Path { get; set; } = @"C:\Users\vano";

        public static string UserName { get; set; } = "vano";

        private const string CompName = "compOfVanoHacker";

        static void Main(string[] args)
        {
            Login.LogIn();

            Init();
            string command = "";

            while (true)
            {
                PrintInfo();
                command = Console.ReadLine();
                if (command == "exit")
                    break;
                int gi = GetNumb(command);
                string[] p = new string[gi];
                command = GetParams(command, ref p);
                Execute(command, p);
            }
        }

        static int GetNumb(string line)
        {
            int answer = 0;

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ' ')
                    answer++;
            }

            return answer;
        }

        static string GetParams(string line, ref string[] p)
        {
            string command = "";
            int index = 0;
            for (int i = 0; i < line.Length; i++)
            {
                command += line[i];
                try
                {
                    if (line[i + 1] == ' ')
                    {
                        index = i + 1;
                        break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    if (line[i] == ' ')
                    {
                        index = i;
                        break;
                    }
                }
            }

            for (int i = index + 1, j = 0; i < line.Length; i++)
            {
                try
                {
                    if (line[i] == ' ')
                    {
                        j++;
                    }
                    else
                    {
                        p[j] += line[i];
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    break;
                }
            }

            return command;
        }

        static void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0}@{1}", UserName, CompName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("{0}", Path);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("{0} ", "#");
        }

        static void Init()
        {
            commands.Add(new CD());
            commands.Add(new LS());
            commands.Add(new Clear());
            commands.Add(new Mkdir());
            commands.Add(new Touch());
        }

        static void Execute(string c, params string[] p)
        {
            bool find = false;
            foreach (var com in commands)
            {
                if (MyEquals(c, com.ProgramName))                                                                                                                                                                                                                //хуй
                {
                    com.Execute(p);
                    find = true;
                }
            }
            if(!find && c != "")
            {
                Console.WriteLine("vanobash: {0}: command not found", c);
            }
        }

        /// <summary>
        /// костыль
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        static bool MyEquals(string first, string second)
        {
            bool answer = true;

            if (first.Length != second.Length)
                return false;

            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }

            return answer;
        }
    }
}
