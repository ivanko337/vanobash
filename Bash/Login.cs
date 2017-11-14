using System;

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

            return answer;
        }

        static string GetPass()
        {
            //Console.Write("password: ");
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
                    Console.Write("*");
                }
            }
            return pwd;
        }
    }
}