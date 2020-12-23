using Sigma.Utils;
using System;
using System.Reflection;

namespace Sigma.Comunication
{
    public sealed class MethodCaller
    {

        private object listener;
        private string methodName;

        public object Listener 
        { 
            get
            {
                return listener;
            } 
            private set 
            {
                listener = value;
            } 
        }

        public string MethodName
        {
            get
            {
                return methodName;
            }
            private set
            {
                methodName = value;
            }
        }

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
            Type listenerType = listener.GetType();
            return listenerType.GetMethod(MethodName, BindingFlags.Public);
        }

        public bool isMethodValid()
        {
            return (GetMethodInfo() != null);
        }
    }

}
