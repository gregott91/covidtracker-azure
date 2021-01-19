using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Interop.Models
{
    public class ProcessConfig
    {
        public string Command { get; set; }

        public string Arguments { get; set; }

        public string WorkingDirectory { get; set; }
    }
}
