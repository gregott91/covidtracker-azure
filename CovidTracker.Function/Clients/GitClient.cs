using CovidTracker.Function.Models;
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

        public async Task CloneAsync(string path, string cloneURL)
        {
            await RunGitCommandAsync(path, $"clone {cloneURL}");
        }

        public async Task StageAsync(string path, string filePath)
        {
            await RunGitCommandAsync(path, $"add {filePath}");
        }

        public async Task CommitAsync(string path, string commitMessage)
        {
            await RunGitCommandAsync(path, $"commit -m \"{commitMessage}\"");
        }

        public async Task PushAsync(string path)
        {
            await RunGitCommandAsync(path, $"push");
        }

        private async Task RunGitCommandAsync(string path, string command)
        {
            await _commandClient.RunAsync("git", command, path);
        }
    }
}
