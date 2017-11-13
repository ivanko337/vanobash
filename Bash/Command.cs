using System;

namespace по_схеме_Сержио
{
    class Command : IExecutable
    {
        public virtual string ProgramName { get; } = "Command";

        public virtual void Execute(params string[] p)
        {
            Console.WriteLine("Type for description command's types");
        }
    }
}
