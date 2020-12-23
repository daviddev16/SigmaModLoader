using System;

namespace Sigma.Logging
{
    public interface IProvider
    {
        void LogBase(string message, Level level, Type logType);
        void LogBase(string message);
    }
}
