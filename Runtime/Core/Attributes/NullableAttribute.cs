using System;

namespace USML {

    /// <summary>
    /// This attribute can be added in methods that can returns a null reference.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class NullableAttribute : Attribute {}
}
