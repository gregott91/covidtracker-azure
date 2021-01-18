using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class GitPagesUploader
    {
        private GitManager _gitManager;
        private FileSystemClient _fileSystemClient;

        public GitPagesUploader(
            GitManager gitManager,
            FileSystemClient fileSystemClient)
        {
            _gitManager = gitManager;
            _fileSystemClient = fileSystemClient;
        }

        public async Task UploadNewFileAsync(
            GitConfig gitConfig,
            string parentDirectory,
            string outputFileName,
            ILoggingClient logger)
        {
            string gitPath = _fileSystemClient.CreateDirectory(parentDirectory, "git");
            await _gitManager.CloneRepoAsync(gitPath, gitConfig.CloneUrl);

            string localRepoPath = _fileSystemClient.GetDirectory(gitPath, gitConfig.RepoName);

            _fileSystemClient.CopyFile(parentDirectory, localRepoPath, outputFileName);
            await _gitManager.CommitFileAsync(localRepoPath, outputFileName, "Updating HTML", logger);
        }
    }
}
