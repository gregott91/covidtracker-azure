using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Logging
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
    }
}
