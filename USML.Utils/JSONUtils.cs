using System;
using System.Collections.Generic;
using System.Json;
using System.Text;

namespace USML {

    public class JSONUtils {
    
        public static JsonValue GetValue(ref JsonObject jObject, string key)
        {
            if(jObject.TryGetValue(key, out JsonValue value)) {
                return value;
            }
            return null;
        }

        public static string GetString(ref JsonObject jObject, string key) 
        {
            string value = Get(ref jObject, key).ToString().Trim();
            return value.Substring(1, value.Length - 2);
        }

        public static object Get(ref JsonObject jObject, string key) 
        {
            JsonValue value = GetValue(ref jObject, key);

            switch(value.JsonType) {
                case JsonType.Array: return (JsonArray)value;
                case JsonType.String: return value.ToString();
            }

            return value;
        }
    
    }
}
