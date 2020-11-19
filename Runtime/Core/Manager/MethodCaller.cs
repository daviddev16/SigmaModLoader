
using System;
using System.Reflection;

namespace Sigma
{
    public sealed class MethodCaller : IValidator {

        private object Listener = null;
        private string Method = null;
        private object[] Parameters = null;

        public MethodCaller(object listener, string method) {
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

        public void SetParameters(params object?[] Parameters)
        {
            this.Parameters = Parameters;
        }

        public bool Validate()
        {

            MethodInfo MethodInfo = GetMethodInfo();
         
            if(MethodInfo !is null)
            {
                if(Parameters !is null)
                {
                    if(Parameters.Length != MethodInfo.GetParameters().Length)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }
    }

}
