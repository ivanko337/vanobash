using System;
using System.Collections.Generic;
using System.IO;

namespace Bash
{
    class Program
    {
        #region Data
        /// <summary>
        /// Список существующих команд
        /// </summary>
        static List<Command> commands = new List<Command>();

        /// <summary>
        /// Текущая директория
        /// </summary>
        public static string Path { get; set; } = @"C:\Users\vgrit";
        public static string OldPath { get; set; } = "";

        /// <summary>
        /// Домашняя директория пользователя
        /// </summary>
        public static string HomeDirectory { get; set; } = @"C:\Users\vgrit";

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

            Console.WriteLine("СПРАВКА\n\tЕсли в пути к файлу или директории содержится пробел,\n\t" +
                "то перед этим пробелом поставьте ^, чтобы система поняла что это полный путь.\n" +
                "\tПример: C:\\Program^ Files");
            
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

        /// <summary>
        /// Выводит начало строки(имя пользователя, имя компьютера, текущую директорию и знак, означающий пользователь группы root или нет)
        /// </summary>
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
            Console.Title = @"vanobash - " + Program.UserName + "@" + Program.CompName + ": " + Path;
        }

        /// <summary>
        /// Ищет имя пользователя в файле с root-пользователями и если пользователь группы root, то будут предоставлены
        /// права суперпользователя и выведена #.
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Вернёт true если пользователь относитеся к группе root, и false
        /// если пользователь не является членом группы root</returns>
        static bool IsUserRoot(string username)
        {
            using (StreamReader sr = new StreamReader(@"E:\Program Files\Vanobash\login\rootusers.txt"))
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

        /// <summary>
        /// Инициализирует элементы листа с командамии экземплярами классов команд.
        /// </summary>
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
            commands.Add(new Rm());
            commands.Add(new Pwd());
            commands.Add(new Wget());
        }

        /// <summary>
        /// Выполняет поиск нужного экземпляра в листе и вызывается переопределённый в каждом экземпляре 
        /// метод Execute.
        /// </summary>
        /// <param name="c">Команда</param>
        /// <param name="p">Массив параметров</param>
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
