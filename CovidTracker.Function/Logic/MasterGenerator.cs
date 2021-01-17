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
    public class MasterGenerator
    {
        private AzurePackageCoordinator _packageCoordinator;
        private GitManager _gitManager;
        private CommandClient _exeClient;
        private FileSystemClient _fileSystemClient;

        public MasterGenerator(
            AzurePackageCoordinator packageCoordinator,
            GitManager gitManager,
            CommandClient exeClient,
            FileSystemClient fileSystemClient)
        {
            _packageCoordinator = packageCoordinator;
            _gitManager = gitManager;
            _exeClient = exeClient;
            _fileSystemClient = fileSystemClient;
        }

        public async Task HandleGenerationAsync(
            AzureArtifactConfiguration artifactConfig,
            GitConfig gitConfig,
            string outputFileName,
            string path,
            ILoggingClient logger)
        {
            string directoryPath = _fileSystemClient.CreateRandomDirectory(path);

            try
            {
                string file = await _packageCoordinator.DownloadPackageAsync(artifactConfig, directoryPath, logger);

                await _exeClient.RunAsync(file, Path.Combine(directoryPath, outputFileName));

                string gitPath = _fileSystemClient.CreateDirectory(directoryPath, "git");
                await _gitManager.CloneRepoAsync(gitPath, gitConfig.CloneUrl);

                string localRepoPath = Path.Combine(gitPath, gitConfig.RepoName);

                _fileSystemClient.CopyFile(directoryPath, localRepoPath, outputFileName);
                await _gitManager.CommitFileAsync(localRepoPath, outputFileName, "Updating HTML", logger);
            }
            finally
            {
                _fileSystemClient.DeleteDirectory(directoryPath);
            }
        }
    }
}
