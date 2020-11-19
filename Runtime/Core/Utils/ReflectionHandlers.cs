using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Sigma
{
    public sealed class ReflectionHandlers
    {

        public static object? CallMethodFromGenericType(object? Object, string method, object?[]? param)
        {
            Type ObjectType = Object.GetType();
            MethodInfo methodInfo = ObjectType.GetMethod(method);
            if(method != null)
            {
                ParameterInfo[] parameterInfo = methodInfo.GetParameters();

                if(param == null)
                {
                    return methodInfo.Invoke(Object, null);
                }

                if(parameterInfo.Length == param.Length)
                {
                    return methodInfo.Invoke(Object, param);
                }
            }
            return null;
        }
     
        public static object? CallMethodFromGenericType(object? Object, MethodInfo methodInfo, object?[]? param)
        {
            ParameterInfo[] parameterInfo = methodInfo.GetParameters();

            if(param == null)
            {
                return methodInfo.Invoke(Object, null);
            }

            if(parameterInfo.Length == param.Length)
            {
                return methodInfo.Invoke(Object, param);
            }
            return null;
        }

        public static object HandleMethodCaller(in MethodCaller caller, ref object[] paramss)
        {
            return CallMethodFromGenericType(caller.GetListener(), caller.GetMethodInfo(), paramss);
        }
    
    }

}
