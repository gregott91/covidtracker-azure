using CovidTracker.Function.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class CommandClient
    {
        public async Task RunAsync(string command, string arguments, string workingDirectory = null)
        {
            var start = new ProcessStartInfo();
            if (!string.IsNullOrEmpty(workingDirectory))
            {
                start.WorkingDirectory = workingDirectory;
            }

            start.Arguments = arguments;
            start.FileName = command;
            start.WindowStyle = ProcessWindowStyle.Hidden;
            start.CreateNoWindow = true;

            using Process proc = Process.Start(start);
            await WaitForExitAsync(proc);

            if (proc.ExitCode != 0)
            {
                throw new CommandException($"Unable to run {command} command {arguments}. Exit code was {proc.ExitCode}", proc.ExitCode);
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
    }
}
