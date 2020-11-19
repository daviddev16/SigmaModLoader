using System;

namespace Sigma
{

    /// <summary>
    /// This attribute helps to indentify the documented classes/structures.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
    [Documented(true)]
    public class DocumentedAttribute : Attribute {

        private bool Done;

        /// <param name="done">if this class/struct is well-documented or complete</param>
        public DocumentedAttribute(bool done) 
        {
            this.Done = done;
        }

        /// <summary>
        /// returns if it's documented.
        /// </summary>
        public bool IsDone() 
        {
            return Done;
        }

    }
}
