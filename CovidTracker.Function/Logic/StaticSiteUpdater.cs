using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Models;
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
    public class StaticSiteUpdater
    {
        private AzurePackageCoordinator _packageCoordinator;
        private StaticSiteGenerator _siteGenerator;
        private FileSystemClient _fileSystemClient;
        private GitPagesUploader _pagesUploader;

        public StaticSiteUpdater(
            AzurePackageCoordinator packageCoordinator,
            StaticSiteGenerator siteGenerator,
            GitPagesUploader pagesUploader,
            FileSystemClient fileSystemClient)
        {
            _packageCoordinator = packageCoordinator;
            _siteGenerator = siteGenerator;
            _fileSystemClient = fileSystemClient;
            _pagesUploader = pagesUploader;
        }

        public async Task GenerateAndUploadAsync(
            AzureArtifactConfiguration artifactConfig,
            GitSessionConfig gitConfig,
            string outputFileName,
            string path,
            ILoggingClient logger)
        {
            string directoryPath = _fileSystemClient.CreateRandomDirectory(path);
            logger.LogInfo($"Working directory: {directoryPath}");

            logger.LogError("TODOGREG " + gitConfig.Password.Length);

            try
            {
                string file = await _packageCoordinator.DownloadPackageAsync(artifactConfig, directoryPath, logger);

                await _siteGenerator.GenerateSiteAsync(file, directoryPath, outputFileName, logger);

                await _pagesUploader.UploadNewFileAsync(gitConfig, directoryPath, outputFileName, logger);
            }
            finally
            {
                try
                {
                    _fileSystemClient.DeleteDirectory(directoryPath);
                }
                catch (Exception ex)
                {
                    logger.LogWarning(ex, $"Unable to delete directory {directoryPath}");
                }
            }
        }
    }
}
