using CovidTracker.Function.Clients;
using CovidTracker.Function.Models;
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

        public async Task CloneRepoAsync(string repoPath, string cloneUrl)
        {
            await _gitClient.CloneAsync(repoPath, cloneUrl);
        }

        public async Task CommitFileAsync(string repoPath, string fileName, string commitMessage, ILoggingClient logger)
        {
            await _gitClient.StageAsync(repoPath, fileName);
            await _gitClient.CommitAsync(repoPath, commitMessage);

            try
            {
                await _gitClient.PushAsync(repoPath);
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
