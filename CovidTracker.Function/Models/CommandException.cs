using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Function.Models
{
    public class CommandException : Exception
    {
        public int ExitCode { get; private set; }

        public CommandException(string message, int exitCode) : base(message)
        {
            ExitCode = exitCode;
        }
    }
}
