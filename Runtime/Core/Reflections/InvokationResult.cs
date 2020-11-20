using System;

namespace Sigma.Reflections
{
    /// <summary>
    /// The return class from an invokation method.<br></br>
    /// Used in <see cref="Handlers">Handlers</see> methods.
    /// </summary>
    ///
    [Documented(true)]
    public struct InvokationResult<E>
    {
        /// <summary>
        /// Invokation failed.
        /// </summary>
        /// 
        public bool Failed { get; private set; }
        /// <summary>
        /// The return of the invokation
        /// </summary>
        /// 
        public E Result { get; private set; }

        /// <summary>
        /// an exception thrown
        /// </summary>
        /// 
        public Exception Exception { get; private set; }

        /// <summary>
        /// Set the result of the invokation
        /// </summary>
        /// <param name="failed">failed status</param>
        /// <param name="resultObject">the result of the invokation</param>
        ///
        public void SetResult(E resultObject, bool failed)
        {
            Result = resultObject;
            Failed = failed;
        }

        /// <summary>
        /// the thrown exception
        /// </summary>
        /// <param name="exception">inner exception if it failed</param>
        ///
        public void SetException(Exception exception)
        {
            Exception = exception;
        }

    }
}
