using System;

namespace Sigma.Logging
{
    public class SigmaProvider : IProvider
    {

        public static readonly string LOGGER_FORMAT = "[{3} -> {1}] [{0}] [at {4}]: {2}";

        public static readonly string DEFAULT_SIGMA_APPLICATION = "SigmaFramework";

        public string Application { get; private set; }

        public SigmaProvider() 
        {
            this.Application = DEFAULT_SIGMA_APPLICATION;
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
            string TimeToString = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
            string LevelToString = level.ToString().ToUpper();
            Console.WriteLine(string.Format(LOGGER_FORMAT, TimeToString, LevelToString, message, Application, logType.Name));
        }

        public void LogBlankLine()
        {
            Console.WriteLine();
        }
    }
}
