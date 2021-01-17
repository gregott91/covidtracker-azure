using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Function.Clients
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
    }
}
