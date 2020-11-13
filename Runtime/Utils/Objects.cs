using System;
using System.Diagnostics.CodeAnalysis;
using System.Json;

namespace USML {

    /// <summary>
    /// This class consists of static utility methods for operating on objects.
    /// </summary>
    [Documented(true)]
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

        /// <summary>
        /// This method returns the JsonValue of the respective key
        /// <br>Warning: this method can return a null object</br>
        /// </summary>
        ///
        /// <returns>The JsonValue</returns>
        /// <param name="jObject">The reference of the JsonObject</param>
        /// <param name="key">The key</param>
        /// 
        [Nullable]
        public static JsonValue GetValue([NotNull] ref JsonObject jObject, string key) {
            if(jObject.TryGetValue(key, out JsonValue value)) {
                return value;
            }
            return null;
        }

        /// <summary>
        /// This method returns the String of the respective key
        /// <br>Warning: this method can return a null object</br>
        /// </summary>
        ///
        /// <returns>The JsonValue</returns>
        /// <param name="jObject">The reference of the JsonObject</param>
        /// <param name="key">The key</param>
        /// 
        [Nullable]
        public static string GetString([NotNull] ref JsonObject jObject, string key) {
            string value = Get(ref jObject, key).ToString().Trim();
            return value.Substring(1, value.Length - 2);
        }

        /// <summary>
        /// This method returns the Object of the respective key
        /// <br>Warning: this method can return a null object</br>
        /// </summary>
        ///
        /// <returns>The JsonValue</returns>
        /// <param name="jObject">The reference of the JsonObject</param>
        /// <param name="key">The key</param>
        /// 
        [Nullable]
        public static object Get([NotNull] ref JsonObject jObject, string key) {
            JsonValue value = GetValue(ref jObject, key);

            switch(value.JsonType) {
                case JsonType.Array: return (JsonArray)value;
                case JsonType.String: return value.ToString();
            }

            return value;
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
