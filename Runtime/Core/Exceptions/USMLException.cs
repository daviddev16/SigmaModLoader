using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace USML {

    public class USMLException : Exception {

        public USMLException() : base() {}

        public USMLException(string message) : base(message) {}

        public USMLException(string message, Exception innerException) : base(message, innerException) {}

        protected USMLException(SerializationInfo info, StreamingContext context) : base(info, context) {}
    }


}
