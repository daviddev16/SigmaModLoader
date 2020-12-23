using System;

namespace Sigma.Utils
{

    /// <summary>
    /// This class consists of static utility methods for operating on objects.
    /// </summary>
    [Documented(true)]
    public class Objects
    {
        
        /// <summary>
        /// This method check if the passed variable is null.
        /// </summary>
        /// <param name="obj">The object that will be checked</param>
        /// <param name="inner">The inner exception</param>
        /// <param name="message">The message of the Exception</param>
        /// 
        /// <exception cref="NullReferenceException">If the actual object is null</exception> 
        /// <returns>The passed object</returns>
        /// 
        public static E RequireNotNull<E>(ref E obj, string message, Exception inner)
        {
            return CheckIsNull(ref obj, true, message, inner);
        }

        /// <summary>
        /// This method check if the passed variable is null.
        /// </summary>
        /// <param name="obj">The object that will be checked</param>
        /// <param name="message">The message of the Exception</param>
        /// 
        /// <exception cref="NullReferenceException">If the actual object is null</exception> 
        /// <returns>The passed object</returns>
        /// 
        public static E RequireNotNull<E>(ref E obj, string message)
        {
            return RequireNotNull(ref obj, message, null);
        }

        /// <summary>
        /// This method check if the passed variable is null.
        /// </summary>
        /// <param name="Num">The current number that will be checked</param>
        /// <param name="message">The thrown message</param>
        /// 
        /// <exception cref="IndexOutOfRangeException">If the number is less than 0</exception> 
        /// <returns>The passed object</returns>
        /// 
        public static void CheckIsPositive(int Num, string message)
        {
            if(Num < 0)
            {
                throw new IndexOutOfRangeException(message);
            }
        }

        private static E CheckIsNull<E>(ref E obj, bool throwException, string message, Exception inner)
        {
            if(obj != null)
            {
                return obj;
            }
            if(obj == null && throwException)
            {
                throw new NullReferenceException(message, inner);
            }
            return default;
        }

    }

}
