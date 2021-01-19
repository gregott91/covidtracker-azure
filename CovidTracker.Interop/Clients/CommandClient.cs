using CovidTracker.Interop.Models;
using CovidTracker.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Interop.Clients
{
    public class CommandClient
    {
        public async Task RunAsync(ProcessConfig config, ILoggingClient logger)
        {
            var start = new ProcessStartInfo();
            if (!string.IsNullOrEmpty(config.WorkingDirectory))
            {
                start.WorkingDirectory = config.WorkingDirectory;
            }

            start.Arguments = config.Arguments;
            start.FileName = config.Command;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;

            using Process proc = Process.Start(start);
            await WaitForExitAsync(proc);

            string stderr = proc.StandardError.ReadToEnd();
            LogOutput("StdOut", config.Command, proc.StandardOutput.ReadToEnd(), logger.LogInfo);
            LogOutput("StdErr", config.Command, stderr, logger.LogInfo);

            if (proc.ExitCode != 0)
            {
                throw new CommandException($"Unable to run {config.Command} command {config.Arguments}. Exit code was {proc.ExitCode}. StdErr: {stderr}", proc.ExitCode);
            }
        }

        private Task WaitForExitAsync(Process process)
        {
            if (process.HasExited) return Task.CompletedTask;

            var tcs = new TaskCompletionSource<object>();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);

            return process.HasExited ? Task.CompletedTask : tcs.Task;
        }

        private void LogOutput(string outputType, string command, string output, Action<string> logAction)
        {
            if (!string.IsNullOrWhiteSpace(output))
            {
                logAction($"Recieved {outputType} from {command}: {output}");
            }
        }
    }
}
