using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Logging
{
    public interface ILoggingClient
    {
        public void LogInfo(string message);

        public void LogError(Exception ex, string message);

        public void LogError(string message);
    }
}
