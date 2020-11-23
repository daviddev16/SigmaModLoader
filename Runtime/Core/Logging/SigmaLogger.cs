using Sigma.Utils;
using System;
using System.Text;

namespace Sigma.Logging
{

    public class SigmaLogger : IValidator
    {

        public static readonly IProvider DEFAULT_PROVIDER = new SigmaProvider();

        public static readonly int TITLE_CHARACTER_LIMIT = 70;

        public Type Type { get; private set; }

        public IProvider Provider { get; private set; }

        public SigmaLogger(Type logType)
        {
            Type = logType;
            Provider = DEFAULT_PROVIDER;
        }

        public SigmaLogger(Type logType, IProvider provider)
        {
            Type = logType;
            Provider = provider;
        }

        public void LogError(string message, Exception e, bool IsCritical = false)
        {
            PreCheckLog();
            Level level = (IsCritical) ? Level.CRITICAL : Level.ERROR;

            Provider.LogBase(message, level, Type);

            if(e != null)
            {
                Provider.LogBase(e.GetType().Name + ": " + e.Message, level, Type);
                LogStackTrace(e.StackTrace.Trim(), level);
            }
        }

        public void LogBlankLine(int height = 1)
        {
            PreCheckLog();
            Provider.LogBlankLine();
        }

        public void LogFail(string message, string reason = "Unknown")
        {
            PreCheckLog();
            Provider.LogBase(message, Level.FAIL, Type);

            if(!reason.Equals("Unknown"))
            {
                Provider.LogBase(reason, Level.FAIL, Type);
            }
        }

        public void LogCritical(string message)
        {
            LogError(message, null);
        }

        public void LogError(string message)
        {
            PreCheckLog();
            LogError(message, null);
        }

        public void LogWarning(string message)
        {
            PreCheckLog();
            Provider.LogBase(message, Level.WARNING, Type);
        }

        public void LogInformation(string message)
        {
            PreCheckLog();
            Provider.LogBase(message, Level.INFO, Type);
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
            PreCheckLog();
            if(StackTrace is null)
            {
                return;
            }
            LogBlankLine(2);
            Provider.LogBase(StackTrace, RootLevel, Type);
            LogBlankLine(2);
        }

        public bool Validate()
        {
            return Provider != null;
        }

        public void PreCheckLog()
        {
            if(!Validate())
            {
                throw new InvalidOperationException("Missing provider in logging.");
            }
        }
    }


}
