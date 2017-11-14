using System;

namespace Bash
{
    class NullPathException : Exception
    {
        public NullPathException() : base("Null directory")
        {

        }
    }
}
