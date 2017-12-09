using System;
using System.Collections.Generic;
using System.IO;

namespace Bash
{
    class Program
    {
        #region Данные
        /// <summary>
        /// Список существующих команд
        /// </summary>
        static List<Command> commands = new List<Command>();

        /// <summary>
        /// Текущая директория
        /// </summary>
        public static string Path { get; set; } = @"C:\Users\vano";
        public static string OldPath { get; set; } = "";

        /// <summary>
        /// Домашняя директория пользователя
        /// </summary>
        public static string HomeDirectory { get; set; } = @"C:\Users\vano";

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string UserName { get; set; }

        /// <summary>
        /// Является ли этот пользователь суперпользователем или нет
        /// </summary>
        public static bool RootOrNotRoot { get; set; } = true;

        /// <summary>
        /// Имя компьютера(требуется вытаскивать это значение через winapi)
        /// </summary>
        public const string CompName = "compOfVanoHacker";
        #endregion

        static void Main(string[] args)
        {
            if (!Login.LogIn())
                return;
            else
                Execute();
        }

        static void Execute()
        {
            Init();
            
            // Входная строка
            string command = "";

            while (true)
            {
                PrintInfo();
                command = Console.ReadLine();
                if (command == "exit")
                    break;
                string commandName = "";
                string[] parameters = ServiceClass.GetParams(command, ref commandName);
                Execute(commandName, parameters);
            }
        }

        static void PrintInfo()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("{0}@{1}", UserName, CompName);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(":");
            Console.ForegroundColor = ConsoleColor.Cyan;
            if (Path == HomeDirectory)
                Console.Write("~");
            else
                Console.Write("{0}", Path);
            Console.ForegroundColor = ConsoleColor.White;
            if (IsUserRoot(UserName))
                Console.Write("{0} ", "#");
            else
                Console.Write("{0} ", "$");
            CD.ChangeDirectory(Path);
        }

        static bool IsUserRoot(string username)
        {
            using (StreamReader sr = new StreamReader(@"D:\Program Files\Vanobash\login\rootusers.txt"))
            {
                string line = sr.ReadLine();
                string hashUserName = Login.GetHash(username);

                while (line != null)
                {
                    if (line == hashUserName)
                        return true;
                    line = sr.ReadLine();
                }
            }
            return false;
        }

        static void Init()
        {
            commands.Add(new CD());
            commands.Add(new LS());
            commands.Add(new Clear());
            commands.Add(new Mkdir());
            commands.Add(new Touch());
            commands.Add(new Cat());
            commands.Add(new UserAdd());
            commands.Add(new CP());
        }

        static void Execute(string c, params string[] p)
        {
            bool find = false;
            foreach (var com in commands)
            {
                if (c.Equals(com.ProgramName))                                                                                                                                                                                                                //хуй
                {
                    com.Execute(p);
                    find = true;
                }
            }
            if (!find && c != "")
            {
                Console.WriteLine("vanobash: {0}: command not found", c);
            }
        }
    }
}
