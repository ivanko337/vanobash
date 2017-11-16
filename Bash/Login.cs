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

            string resultLognPass = login + ":" + pass;
            resultLognPass = GetHash(resultLognPass);

            bool result = false;
            for(int i = 0; i < 2; i++)
            {
                Console.Write("Password: ");
                pass = GetPass();
                resultLognPass = login + ":" + pass;
                resultLognPass = GetHash(resultLognPass);
                if (FindLine(resultLognPass))
                {
                    result = true;
                    break;
                }
            }
            
            return result;
        }

        /// <summary>
        /// Возвращает hash, зашифрованный по алгоритму md5
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
            if (Directory.Exists(@"D:\Program Files\Vanobash"))
            {
                CreateFiles();
            }
            else
            {
                Directory.CreateDirectory(@"D:\Program Files\Vanobash");
                CreateFiles();
            }
        }

        static void CreateFiles()
        {
            if (!File.Exists(@"D:\Program Files\Vanobash\pass.txt"))
            {
                using (File.Create(@"D:\Program Files\Vanobash\pass.txt"))
                { }
                using (StreamWriter stw = new StreamWriter(@"D:\Program Files\Vanobash\pass.txt"))
                { stw.WriteLine(GetHash("guest:guest")); }
            }
            else
            {
                if(!FindLine(GetHash("guest:guest")))
                {
                    using (StreamWriter stw = new StreamWriter(@"D:\Program Files\Vanobash\pass.txt"))
                    { stw.WriteLine(GetHash("guest:guest")); }
                }
            }
            if (!File.Exists(@"D:\Program Files\Vanobash\user.txt"))
            {
                using (File.Create(@"D:\Program Files\Vanobash\user.txt"))
                { }
                using (StreamWriter stw = new StreamWriter(@"D:\Program Files\Vanobash\user.txt"))
                { stw.WriteLine("guest"); }
            }
            else
            {
                if (!FindLine("guest"))
                {
                    using (StreamWriter stw = new StreamWriter(@"D:\Program Files\Vanobash\user.txt"))
                    { stw.WriteLine(GetHash("guest")); }
                }
            }
        }

        static bool FindLine(string hash)
        {
            //меняйте под себя директорию, ибо пока что это костыль
            using (StreamReader str = new StreamReader(@"D:\Program Files\Vanobash\pass.txt"))
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

        static string GetPass()
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
                        Console.Write("\b \b");
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