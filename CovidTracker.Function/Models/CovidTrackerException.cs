using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Function.Models
{
    public class CovidTrackerException : Exception
    {
        public CovidTrackerException(string message) : base(message)
        {}
    }
}
