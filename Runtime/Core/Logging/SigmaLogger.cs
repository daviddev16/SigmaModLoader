using System;

namespace Sigma
{

    public class SigmaLogger {

        public enum Level
        {
            FAIL,
            CRITICAL,
            INFO,
            ERROR,
            WARNING
        }

        public static void LogError(string message, Exception e, bool IsCritical = false) 
        {
            Level level = (IsCritical) ? Level.CRITICAL : Level.ERROR;
            
            LogBase(message, level, true);

            if(e != null)
            {
                LogBase(e.Message, level, true);
                LogStackTrace(e.StackTrace, level);
            }
        }

        public static void LogFail(string message, string reason = "Unknown") 
        {
            LogBase(message, Level.FAIL, true);
         
            if(!reason.Equals("Unknown"))
            {
                LogBase(reason, Level.FAIL, false);
            }
        }

        public static void LogCritical(string message)
        {
            LogError(message, null, true);
        }

        public static void LogError(string message) 
        {
            LogError(message, null, false);
        }

        public static void LogWarning(string message)
        {
            LogBase(message, Level.WARNING, true);
        }

        public static void LogInformation(string message)
        {
            LogBase(message, Level.INFO, true);
        }

        private static void LogStackTrace(string StackTrace, Level RootLevel)
        {

            if(StackTrace is null)
            {
                return;
            }

            foreach(string line in StackTrace.Split("\n"))
            {
                if(!string.IsNullOrEmpty(line))
                {
                    LogBase(line, RootLevel, false);
                }
            }
        }

        public static void LogBase(string message, Level level, bool WriteLevel) 
        {
            Console.WriteLine(message);
        }
    
    }


}
