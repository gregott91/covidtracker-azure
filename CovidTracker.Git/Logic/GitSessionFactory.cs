using CovidTracker.Git.Clients;
using CovidTracker.Git.Models;
using CovidTracker.Interop.Clients;
using CovidTracker.Logging;
using System.Threading.Tasks;

namespace CovidTracker.Git.Logic
{
    public static class GitSessionFactory
    {
        public static async Task<GitSession> OpenSessionAsync(GitSessionConfig config, string cloneLocation, ILoggingClient logger)
        {
            var session = new GitSession(
                new GitClient(new CommandClient()),
                new FileSystemClient(),
                logger);

            await session.InitializeAsync(config, cloneLocation);

            return session;
        }
    }
}
