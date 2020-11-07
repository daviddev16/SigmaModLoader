using System;
using System.Collections.Generic;
using System.Text;

namespace USML {

    public class Objects {

        private static E CheckIsNull<E>(ref E obj, bool throwException, string message, Exception inner)
        {
            if(obj != null) {
                return obj;
            }
            if(obj == null && throwException) 
            {
                if(inner == null) 
                {
                    throw new NullReferenceException(message);
                }
                throw new NullReferenceException(message, inner);
            }

            return default;
        }

        public static E RequireNotNull<E>(ref E obj, string message, Exception inner) 
        {
            return CheckIsNull(ref obj, true, message, inner);
        }

        public static E RequireNotNull<E>(E obj) 
        {
            return RequireNotNull(ref obj, "This object cannot be null.", null);
        }

        public static E RequireNotNull<E>(ref E obj, string message) 
        {
            return RequireNotNull(ref obj, message, null);
        }
    
    }

}
