using CovidTracker.Function.Models;
using CovidTracker.Git.Clients;
using CovidTracker.Git.Models;
using CovidTracker.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class GitClient
    {
        private CommandClient _commandClient;

        public GitClient(CommandClient commandClient)
        {
            _commandClient = commandClient;
        }

        public async Task CloneAsync(string path, string cloneURL, ILoggingClient logger)
        {
            await RunGitCommandAsync("asd", "asd", path, $"clone {cloneURL}", logger);
        }

        public async Task StageAsync(string path, string filePath, ILoggingClient logger)
        {
            await RunGitCommandAsync("asd", "asd", path, $"add {filePath}", logger);
        }

        public async Task CommitAsync(string path, string commitMessage, ILoggingClient logger)
        {
            await RunGitCommandAsync("asd", "asd", path, $"commit -m \"{commitMessage}\"", logger);
        }

        public async Task PushAsync(string path, ILoggingClient logger)
        {
            await RunGitCommandAsync("asd", "asd", path, $"push", logger);
        }

        private async Task RunGitCommandAsync(string username, string password, string path, string command, ILoggingClient logger)
        {
            await _commandClient.RunAsync(new ProcessConfig()
            {
                Command = "git",
                Arguments = command,
                WorkingDirectory = path
            }, logger);
        }
    }
}
