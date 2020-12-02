using System;
using System.Reflection;
using Sigma.Comunication;
using Sigma.Logging;
using Sigma.Manager;
using Sigma.Utils;

namespace Sigma.Reflections
{
    /// <summary>
    /// This class contains every function that can be used for Reflections with Methods/Types
    /// </summary>
    /// 
    [Documented(true)]
    public sealed class Handlers
    {

        private readonly static SigmaLogger Logger = new SigmaLogger(typeof(Handlers));

        /// <summary>
        /// Call method using reflections with parameters.
        /// </summary>
        /// 
        /// <param name="methodInfo">The MethodInfo to invoke</param>
        /// <param name="obj">The actual object that will be used to invoke the method</param>
        /// <param name="param">The arguments passed to the method</param>
        /// 
        /// <returns>
        /// This method will return an <see cref="InvokationResult{E}"/> as the Result given a Failed and Return Value
        /// of the invokation
        /// </returns>
        ///
        /// <exception cref="NullReferenceException">if the Listener/MethodInfo is null</exception>
        /// <exception cref="NullReferenceException">Missing arguments or null array</exception>
        /// <exception cref="InvalidOperationException">Missing parameters or Parameter limit exceeded</exception>
        /// 
        /// Used internally in <seealso cref="ModManagerSystem.CallSequencer(Sequencer, object[])"/>.
        ///
        public static InvokationResult<object> CallMethod(object obj, MethodInfo methodInfo, object[] param = null)
        {
            InvokationResult<object> Sender = new InvokationResult<object>();
            try
            {
                Objects.RequireNotNull(ref obj, "Listener object cannot be null.");
                Objects.RequireNotNull(ref methodInfo, "MethodInfo name cannot be null.");

                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                object returnValue = null;

                /*if the current method has any parameter*/
                if(parameterInfos.Length > 0)
                {
                    if(param == null)
                    {
                        throw new NullReferenceException("missing " + parameterInfos.Length + " arguments.");
                    }
                    /*if it's missing some arguments*/
                    else if(parameterInfos.Length > param.Length)
                    {
                        throw new InvalidOperationException("Missing parameters.");
                    }
                    /*Check if the param passed is greater than the actual method*/
                    if(parameterInfos.Length < param.Length)
                    {
                        throw new InvalidOperationException("parameter limit exceeded.");
                    }
                    returnValue = methodInfo.Invoke(obj, param);
                }
                else
                {
                    /*Unecessary alert*/
                    if(param != null)
                    {
                        Logger.LogWarning("This method doesn't require any parameters.");
                    }
                    returnValue = methodInfo.Invoke(obj, null);
                }
                
                Sender.SetResult(returnValue, false);
            }
            catch(Exception e)
            {
                Sender.SetException(e);
                Sender.SetResult(null, true);
            }
            
            return Sender;
        }

        /// <summary>
        /// Invoke a MethodCaller this method use <see cref="Call(MethodCaller, ref object[])"/> function to work.
        /// </summary>
        /// 
        /// <param name="caller">The MethodCaller</param>
        /// <param name="parameters">the method parameters</param>
        /// 
        /// <returns>
        /// The <see cref="InvokationResult{E}"/> as the return
        /// of the invokation
        /// </returns>
        /// 
        /// Used internally in <seealso cref="ModManagerSystem.CallSequencer(Sequencer, object[])"/>.
        ///
        public static InvokationResult<object> Call(MethodCaller caller, ref object[] parameters)
        {
            Objects.RequireNotNull(ref caller, "the passed MethodCaller is null");
            return CallMethod(caller.GetListener(), caller.GetMethodInfo(), parameters);
        }

    }

}
