using System;

namespace Sigma.Logging
{
    public class SigmaProvider : IProvider
    {

        public static readonly string LOGGER_FORMAT = "[{3} -> {1}] [{0}] [at {4}]: {2}";

        public string Application { get; private set; }

        public SigmaProvider() { }

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
