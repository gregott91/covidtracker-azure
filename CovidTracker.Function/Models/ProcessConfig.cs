using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Function.Models
{
    public class ProcessConfig
    {
        public string Command { get; set; }

        public string Arguments { get; set; }

        public string WorkingDirectory { get; set; }
    }
}
