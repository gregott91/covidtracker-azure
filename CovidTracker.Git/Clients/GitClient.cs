using CovidTracker.Git.Models;
using CovidTracker.Interop.Clients;
using CovidTracker.Interop.Models;
using CovidTracker.Logging;
using System.Threading.Tasks;

namespace CovidTracker.Git.Clients
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
            await RunGitCommandAsync(path, $"clone {cloneURL}", logger);
        }

        public async Task SetOrigin(string path, string originUrl, ILoggingClient logger)
        {
            await RunGitCommandAsync(path, $"remote set-url origin {originUrl}", logger);
        }

        public async Task SetUserProperty(string path, string property, string value, ILoggingClient logger)
        {
            await RunGitCommandAsync(path, $"config user.{property} \"{value}\"", logger);
        }

        public async Task StageAsync(string path, string filePath, ILoggingClient logger)
        {
            await RunGitCommandAsync(path, $"add {filePath}", logger);
        }

        public async Task CommitAsync(string path, string commitMessage, ILoggingClient logger)
        {
            await RunGitCommandAsync(path, $"commit -m \"{commitMessage}\"", logger);
        }

        public async Task PushAsync(string path, ILoggingClient logger)
        {
            await RunGitCommandAsync(path, $"push", logger);
        }

        private async Task RunGitCommandAsync(string path, string command, ILoggingClient logger)
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
