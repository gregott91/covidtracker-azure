using CovidTracker.Logging;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Function.Clients
{
    public class FunctionLoggingClient : ILoggingClient
    {
        private ILogger _log;

        public FunctionLoggingClient(ILogger log)
        {
            _log = log;
        }

        public void LogInfo(string message)
        {
            _log.LogInformation(message);
        }

        public void LogError(Exception ex, string message)
        {
            _log.LogError(ex, message);
        }

        public void LogError(string message)
        {
            _log.LogError(message);
        }

        public void LogWarning(string message)
        {
            _log.LogWarning(message);
        }

        public void LogWarning(Exception ex, string message)
        {
            _log.LogWarning(ex, message);
        }
    }
}
