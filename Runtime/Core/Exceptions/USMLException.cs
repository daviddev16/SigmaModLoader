using System;
using System.Runtime.Serialization;

namespace Sigma
{

    public class USMLException : Exception {

        public USMLException() : base() {}

        public USMLException(string message) : base(message) {}

        public USMLException(string message, Exception innerException) : base(message, innerException) {}

        protected USMLException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }


}
