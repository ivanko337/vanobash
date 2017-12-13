using System;
using System.Net;

namespace Bash
{
    sealed class Wget : Command
    {
        public override string ProgramName => "wget";

        public override void Execute(params string[] p)
        {
            if (p.Length == 0)
            {
                Console.WriteLine("vanobash: wget: Operands was not specify.");
            }
            else if (p.Length == 1)
            {
                Download(p[0], Program.Path + ServiceClass.GetFileName(p[0]));
            }
            else if (p.Length == 2)
            {
                Download(p[0], p[1]);
            }
        }

        private void Download(string link, string fileName)
        {
            try
            {
                WebClient myWebClient = new WebClient();
                myWebClient.DownloadFile(link, fileName);
            }
            catch(WebException)
            {
                Console.WriteLine("vanobash: wget: 404 file not found.");
            }
        }
    }
}
