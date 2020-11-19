using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using YamlDotNet.Serialization;

namespace Sigma
{

    /// <summary>
    /// This class consists of static utility methods for operating on objects.
    /// </summary>
    [Documented(false)]
    public class Objects {

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
        /// 
        /// <exception cref="NullReferenceException">If the actual object is null</exception> 
        /// <returns>The passed object</returns>
        /// 
        public static E RequireNotNull<E>(E obj) 
        {
            return RequireNotNull(ref obj, "This object cannot be null.", null);
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

        public static Dictionary<string, object> ReadYAMLFile([NotNull] string FilePath)
        {
            var deserializer = new Deserializer();
            FileStream stream = new FileStream(FilePath, FileMode.Open);
            return deserializer.Deserialize<Dictionary<string, object>>(new StreamReader(stream));
        }

       

        private static E CheckIsNull<E>(ref E obj, bool throwException, string message, Exception inner) {
            if(obj != null) {
                return obj;
            }
            if(obj == null && throwException) {
                if(inner == null) {
                    throw new NullReferenceException(message);
                }
                throw new NullReferenceException(message, inner);
            }

            return default;
        }


    }

}
