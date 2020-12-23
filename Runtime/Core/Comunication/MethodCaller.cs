using System;
using System.Reflection;

namespace Sigma.Comunication
{
    public sealed class MethodCaller
    {
        public object Listener { get; private set; }

        public string MethodName { get; private set; }

        public MethodCaller(object listener, string methodName)
        {
            Listener = listener;
            MethodName = methodName.Trim();
        }

        public MethodInfo GetMethodInfo()
        {
            if(Listener == null || string.IsNullOrEmpty(MethodName))
            {
                return null;
            }
            Type listenerType = Listener.GetType();
            return listenerType.GetMethod(MethodName, BindingFlags.Public);
        }

        public bool isMethodValid()
        {
            return (GetMethodInfo() != null);
        }
    }

}
