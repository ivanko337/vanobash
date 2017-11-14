using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace Bash
{
    public class Login
    {
        public static bool LogIn()
        {
            bool answer = false;

            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string pass = GetPass();
            Console.WriteLine(pass);

            string resultLognPass = login + ":" + pass;

            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(resultLognPass));
            byte[] hash = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                str.Append(hash[i].ToString("x2"));
            }
            Console.WriteLine(str.ToString());

            return answer;
        }

        static bool FindUser(string hash)
        {
            //меняйте под себя директорию, ибо пока что это костыль
            using (StreamReader str = new StreamReader(@"C:\Users\vano\source\repos\Bash\Bash\login\pass.txt"))
            {
                string line = str.ReadLine();

                while (line != null)
                {
                    if (line == hash)
                        return true;
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