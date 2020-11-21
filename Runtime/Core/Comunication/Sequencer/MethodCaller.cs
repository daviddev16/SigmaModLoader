using Sigma.Utils;
using System.Reflection;

namespace Sigma.Comunication
{
    public sealed class MethodCaller : IValidator
    {

        private object Listener = null;
        private string Method = null;

        public MethodCaller(object listener, string method)
        {
            Listener = listener;
            Method = method;
        }

        public object GetListener()
        {
            return Listener;
        }

        public string GetMethodName()
        {
            return Method.Trim();
        }

        public MethodInfo GetMethodInfo()
        {
            if(GetListener() == null || string.IsNullOrEmpty(GetMethodName()))
            {
                return null;
            }
            return GetListener().GetType().GetMethod(GetMethodName());
        }

        public bool Validate()
        {
            return (GetMethodInfo() != null);
        }
    }

}
