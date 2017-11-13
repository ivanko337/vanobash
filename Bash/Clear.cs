using System;

namespace по_схеме_Сержио
{
    sealed class Clear : Command
    {
        public override string ProgramName { get; } = "clear";

        public override void Execute(params string[] p)
        {
            Console.Clear();
        }
    }
}
