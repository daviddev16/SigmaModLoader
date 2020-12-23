using System;
using UnityEngine;

namespace Sigma.Logging
{
    public class UnityProvider : IProvider
    {
        public void LogBase(string message, Level level, Type logType)
        {
            if(level == Level.INFO)
            {
                Debug.Log(message);
            }
            else if(level == Level.WARNING)
            {
                Debug.LogWarning(message);
            }
            else if(level == Level.ERROR || level == Level.CRITICAL)
            {
                Debug.LogError(message);
            }
            else
            {
                Debug.LogWarning(message);
            }
        }

        public void LogBase(string message)
        {
            Debug.Log(message);
        }

        public void LogBlankLine()
        {
            Debug.Log("");
        }
    }
}