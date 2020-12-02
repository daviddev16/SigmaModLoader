using System;

namespace Sigma
{

    public class USMLException : Exception
    {

        public USMLException() : base()
        {
        }

        public USMLException(string message)
            : base(message)
        {
        }

        public USMLException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
