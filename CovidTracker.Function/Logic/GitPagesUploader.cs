using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Models;
using CovidTracker.Git.Logic;
using CovidTracker.Git.Models;
using CovidTracker.Interop.Clients;
using CovidTracker.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class GitPagesUploader
    {
        private FileSystemClient _fileSystem;

        public GitPagesUploader(FileSystemClient fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public async Task UploadNewFileAsync(
            GitSessionConfig gitConfig,
            string fileLocation,
            string fileName,
            ILoggingClient logger)
        {
            string cloneDirectory = _fileSystem.CreateRandomDirectory(fileLocation);

            using var session = await GitSessionFactory.OpenSessionAsync(gitConfig, cloneDirectory, logger);
            
            _fileSystem.CopyFile(fileLocation, session.GetRepoLocation(), fileName);
            await session.StageAsync(fileName);
            await session.SafeCommitAsync("Updating JSON");
            await session.SafePushAsync();
        }
    }
}
