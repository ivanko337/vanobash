using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Bash
{
    class Login
    {
        public static bool LogIn()
        {
            ExistFilesAndDirictoryes();

            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPass();

            string resultLoginPass = login + ":" + pass;
            resultLoginPass = GetHash(resultLoginPass);
            if (FindLine(resultLoginPass))
            {
                Program.UserName = login;
                return true;
            }

            bool result = false;
            for (int i = 0; i < 2; i++)
            {
                Console.Write("Password: ");
                pass = GetPass();
                resultLoginPass = login + ":" + pass;
                resultLoginPass = GetHash(resultLoginPass);
                if (FindLine(resultLoginPass))
                {
                    result = true;
                    break;
                }
            }
            
            return result;
        }

        /// <summary>
        /// Возвращает хэш, зашифрованный по алгоритму md5
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string GetHash(string line)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(line));
            byte[] hash = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("x2"));
            }
            return str.ToString();
        }

        /// <summary>
        /// Если отсутствует папка и файлами для vanobash, то создаются 
        /// нужные директории и файлы
        /// </summary>
        static void ExistFilesAndDirictoryes()
        {
            if (Directory.Exists(@"E:\Program Files\Vanobash\login"))
            {
                CreateFiles();
            }
            else
            {
                Directory.CreateDirectory(@"E:\Program Files\Vanobash\login");
                CreateFiles();
            }
        }

        /// <summary>
        /// Если отсутсвуют файл с паролями и/или файл с пользователями группы root, то этот метод их создаст
        /// и заполнит дефолтными значениями(для файла user - ничего, для файла pass -  guest:guest)
        /// Если файлы существуют, но в них нет стандартных значений, то он их добавит.
        /// </summary>
        static void CreateFiles()
        {
            if (!File.Exists(@"E:\Program Files\Vanobash\login\pass.txt"))
            {
                using (File.Create(@"E:\Program Files\Vanobash\login\pass.txt"))
                { }
                using (StreamWriter stw = new StreamWriter(@"E:\Program Files\Vanobash\login\pass.txt"))
                { stw.WriteLine(GetHash("guest:guest")); }
            }
            else
            {
                if(!FindLine(GetHash("guest:guest")))
                {
                    using (StreamWriter stw = new StreamWriter(@"E:\Program Files\Vanobash\login\pass.txt"))
                    { stw.WriteLine(GetHash("guest:guest")); }
                }
            }
            if (!File.Exists(@"E:\Program Files\Vanobash\user.txt"))
            {
                using (File.Create(@"E:\Program Files\Vanobash\user.txt"))
                { }
                using (StreamWriter stw = new StreamWriter(@"E:\Program Files\Vanobash\user.txt"))
                { stw.WriteLine("guest"); }
            }
            else
            {
                if (!FindLine("guest"))
                {
                    using (StreamWriter stw = new StreamWriter(@"E:\Program Files\Vanobash\user.txt"))
                    { stw.WriteLine(GetHash("guest")); }
                }
            }
        }

        static bool FindLine(string hash)
        {
            //меняйте под себя директорию, ибо пока что это костыль
            using (StreamReader str = new StreamReader(@"E:\Program Files\Vanobash\login\pass.txt"))
            {
                string line = str.ReadLine();

                while (line != null)
                {
                    if (line == hash)
                        return true;
                    line = str.ReadLine();
                }

                return false;
            }
        }

        public static string GetPass()
        {
            string pwd = "";
            while (true)
            {
                ConsoleKeyInfo i = Console.ReadKey(true);
                if (i.Key == ConsoleKey.Enter)
                {
                    Console.Write('\n');
                    break;
                }
                else if (i.Key == ConsoleKey.Backspace)
                {
                    if (pwd.Length != 0)
                    {
                        pwd = pwd.Remove(pwd.Length - 1);
                        //Console.Write("\b \b");
                    }
                }
                else
                {
                    pwd += i.KeyChar;
                    Console.Write("");
                }
            }
            return pwd;
        }
    }
}