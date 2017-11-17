using System;
using System.IO;

namespace Bash
{
    sealed class UserAdd : Command
    {
        public override string ProgramName => "useradd";

        public override string ManFilePath => @"D:\Program Files\Vanobash\Man\Useradd.txt";

        private string UserName { get; set; }
        private string UserPassword { get; set; }

        public override void Execute(params string[] p)
        {
            // try-catch блок для отлова IndexOutOfRangeException для первого аргумента
            // (вызывается если после команды useradd не указано имя пользователя, а так же пробела,
            // если будет пробел, то p[0] будет равен null
            try
            {
                if(p[0] == null)
                {
                    RecordPassAndName();
                    Write(UserName, UserPassword);
                    return;
                }

                // try-catch блок для отлова IndexOutOfRangeException для второго аргумента
                // (вызывается если после имени пользователя не написан пароль, а так же пробела,
                // если будет пробел, то p[1] будет равен null)
                try
                {
                    if(p[1] == null)
                    {
                        RecordPass();
                        Write(UserName, UserPassword);
                        return;
                    }
                }
                catch(IndexOutOfRangeException)
                {
                    RecordPass();
                    Write(UserName, UserPassword);
                    return;
                }
            }
            catch(IndexOutOfRangeException)
            {
                RecordPassAndName();
                Write(UserName, UserPassword);
                return;
            }
        }

        private void RecordPassAndName()
        {
            Console.Write("Set username: ");
            UserName = Console.ReadLine();
            RecordPass();
        }

        private void RecordPass()
        {
            Console.Write("Set password to {0}: ", UserName);
            UserPassword = Login.GetPass();
            Console.Write("Confirm password: ");
            string temp = Login.GetPass();
            for (int i = 0; i < 2; i++)
            {
                if (temp == UserPassword)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Passwords did not match");
                    Console.Write("Confirm password: ");
                    temp = Login.GetPass();
                    if (i == 1)
                        return;
                }
            }
        }

        static void Write(string user, string pass)
        {
            using (StreamWriter sw = new StreamWriter(@"D:\Program Files\Vanobash\login\pass.txt", true))
            { sw.WriteLine(Login.GetHash(user + ":" + pass)); }
        }
    }
}
