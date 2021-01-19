using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Logging
{
    public class ConsoleLoggingClient : ILoggingClient
    {
        public void LogInfo(string message)
        {
            Console.WriteLine($"Info: {message}");
        }

        public void LogError(Exception ex, string message)
        {
            Console.WriteLine($"Error: {message}. {ex.StackTrace}");
        }

        public void LogError(string message)
        {
            Console.WriteLine($"Error: {message}");
        }

        public void LogWarning(string message)
        {
            Console.WriteLine($"Warning: {message}");
        }

        public void LogWarning(Exception ex, string message)
        {
            Console.WriteLine($"Warning: {message}. {ex.StackTrace}");
        }
    }
}
