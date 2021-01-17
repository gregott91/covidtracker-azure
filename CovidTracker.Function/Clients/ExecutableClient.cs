using CovidTracker.Function.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CovidTracker.Function.Clients
{
    public class ExecutableClient
    {
        public void RunExe(string exePath, string arguments)
        {
            var start = new ProcessStartInfo();
            start.Arguments = arguments;
            start.FileName = exePath;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            int exitCode;

            using Process proc = Process.Start(start);
            proc.WaitForExit();
            exitCode = proc.ExitCode;

            if (exitCode != 0)
            {
                throw new CovidTrackerException($"Unable to run EXE. Exit code was {exitCode}");
            }
        }
    }
}
