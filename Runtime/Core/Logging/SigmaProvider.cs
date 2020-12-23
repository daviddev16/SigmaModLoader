using System;

namespace Sigma.Logging
{
    public class SigmaProvider : IProvider
    {

        public static readonly string LOGGER_FORMAT = "[{3} -> {1}] [{0}] [at {4}]: {2}";

        public static readonly string SIGMA_APPLICATION_NAME = "SigmaFramework";

        public string Application { get; private set; }

        public SigmaProvider() 
        {
            this.Application = SIGMA_APPLICATION_NAME;
        }

        public SigmaProvider(string Application)
        {
            this.Application = Application;
        }

        public void SetApplicationName(string Name)
        {
            this.Application = Name;
        }

        public void LogBase(string message, Level level, Type logType)
        {
            LogBase(string.Format(LOGGER_FORMAT, DateTime.Now.ToString("MM/dd/yyyy H:mm:ss"), 
                level.ToString().ToUpper(), message, Application, logType.Name));
        }

        public void LogBase(string message)
        {
            Console.WriteLine(message);
        }

        public void LogBlankLine()
        {
            LogBase("");
        }

    }
}
