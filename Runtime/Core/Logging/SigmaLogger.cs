using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Sigma
{

    public class SigmaLogger
    {
        
        public static readonly string LOGGER_FORMAT = "[{3} -> {1}] [{0}] [at {4}]: {2}";

        public static readonly int TITLE_CHARACTER_LIMIT = 70;

        public enum Level
        {
            FAIL,
            CRITICAL,
            INFO,
            ERROR,
            WARNING
        }

        public Type Type { get; private set; }

        public string Application { get; private set; }

        public SigmaLogger([NotNull] Type logType)
        {
            Type = logType;
            Application = "Sigma";
        }

        public SigmaLogger([NotNull] Type logType, [NotNull] string Application)
        {
            this.Type = logType;
            this.Application = Application;
        }

        public void LogError(string message, Exception e, bool IsCritical = false)
        {
            Level level = (IsCritical) ? Level.CRITICAL : Level.ERROR;

            LogBase(message, level);

            if(e != null)
            {
                LogBase(e.GetType().Name + ": " + e.Message, level);
                LogStackTrace(e.StackTrace.Trim(), level);
            }
        }

        public void LogBlankLine(int height = 1)
        {
            for(int i = 0; i < height; i++)
            {
                Console.WriteLine();
            }
        }

        public void LogFail(string message, string reason = "Unknown")
        {
            LogBase(message, Level.FAIL);

            if(!reason.Equals("Unknown"))
            {
                LogBase(reason, Level.FAIL);
            }
        }

        public void LogCritical(string message)
        {
            LogError(message, null);
        }

        public void LogError(string message)
        {
            LogError(message, null);
        }

        public void LogWarning(string message)
        {
            LogBase(message, Level.WARNING);
        }

        public void LogInformation(string message)
        {
            LogBase(message, Level.INFO);
        }

        public void LogTitle(string title)
        {
            title = " " + title + " ";
            int Spaces = (TITLE_CHARACTER_LIMIT/2) - title.Length;

            StringBuilder TitleBuilder = new StringBuilder()
                .Append("#");
            DrawChar(ref TitleBuilder, ' ', Spaces)
                .Append(title);
            DrawChar(ref TitleBuilder, ' ', Spaces-2)
                .Append("#");

            StringBuilder LineBuilder = new StringBuilder();
            DrawChar(ref LineBuilder, '#', (Spaces*2) + title.Length);

            LogInformation(LineBuilder.ToString().Trim());
            LogInformation(TitleBuilder.ToString().Trim());
            LogInformation(LineBuilder.ToString().Trim());
        }

        private StringBuilder DrawChar(ref StringBuilder builder, char chr, int times)
        {
            for(int i = 0; i < times; i++)
            {
                builder.Append(chr);
            }
            return builder;
        }

        private void LogStackTrace(string StackTrace, Level RootLevel)
        {

            if(StackTrace is null)
            {
                return;
            }
            LogBlankLine(2);
            LogBase(StackTrace, RootLevel);
            LogBlankLine(2);
        }

        public void LogBase(string message, Level level)
        {
            string TimeToString = DateTime.Now.ToString("MM/dd/yyyy H:mm:ss");
            string LevelToString = level.ToString().ToUpper();
            Console.WriteLine(string.Format(LOGGER_FORMAT, TimeToString, LevelToString, message, Application, Type.Name));
        }

    }


}
