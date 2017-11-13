using System;

namespace по_схеме_Сержио
{
    class NullPathException : Exception
    {
        public NullPathException() : base("Null directory")
        {

        }
    }
}
