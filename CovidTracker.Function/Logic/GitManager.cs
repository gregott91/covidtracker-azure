using CovidTracker.Function.Clients;
using CovidTracker.Function.Models;
using CovidTracker.Git.Models;
using CovidTracker.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class GitManager
    {
        private GitClient _gitClient;

        public GitManager(GitClient gitClient)
        {
            _gitClient = gitClient;
        }

        public async Task CloneRepoAsync(string repoPath, string cloneUrl, ILoggingClient logger)
        {
            await _gitClient.CloneAsync(repoPath, cloneUrl, logger);
        }

        public async Task CommitFileAsync(string repoPath, string fileName, string commitMessage, ILoggingClient logger)
        {
            await _gitClient.StageAsync(repoPath, fileName, logger);
            await _gitClient.CommitAsync(repoPath, commitMessage, logger);

            try
            {
                await _gitClient.PushAsync(repoPath, logger);
            }
            catch (CommandException ex)
            {
                if (ex.ExitCode == 1)
                {
                    logger.LogError("Unable to push - exit code is 1. Likely no changes to push.");
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
