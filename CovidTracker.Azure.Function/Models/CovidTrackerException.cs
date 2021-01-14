using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Azure.Function.Models
{
    public class CovidTrackerException : Exception
    {
        public CovidTrackerException(string message) : base(message)
        {}
    }
}
