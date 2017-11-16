using System;

namespace Bash
{
    class Command : IExecutable
    {
        public virtual string ProgramName { get; } = "Command";

        public virtual string ManFilePath { get; } = @"D:\Program Files\Vanobash\Man\Command.txt";

        public virtual void Execute(params string[] p)
        {
            Console.WriteLine("Type for description command's types");
        }
    }
}
