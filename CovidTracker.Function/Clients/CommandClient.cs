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

            using Process proc = Process.Start(start);
            await WaitForExitAsync(proc);


            logger.LogInfo($"Recieved StdOut from {config.Command}: {proc.StandardOutput.ReadToEnd()}"); 
            logger.LogError($"Recieved StdErr from {config.Command}: {proc.StandardError.ReadToEnd()}");

            if (proc.ExitCode != 0)
            {
                throw new CommandException($"Unable to run {config.Command} command {config.Arguments}. Exit code was {proc.ExitCode}", proc.ExitCode);
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
