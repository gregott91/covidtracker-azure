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
            GitConfig gitConfig,
            string outputFileName,
            string path,
            ILoggingClient logger)
        {
            string directoryPath = _fileSystemClient.CreateRandomDirectory(path);
            logger.LogInfo($"Working directory: {directoryPath}");

            try
            {
                string file = await _packageCoordinator.DownloadPackageAsync(artifactConfig, directoryPath, logger);

                var files = Directory.GetFiles(Directory.GetParent(file).FullName);
                foreach(var outputFile in files)
                {
                    logger.LogInfo("TODOGREG GOT THING " + outputFile);
                }

                await _siteGenerator.GenerateSiteAsync(file, directoryPath, outputFileName);

                await _pagesUploader.UploadNewFileAsync(gitConfig, directoryPath, outputFileName, logger);
            }
            finally
            {
                _fileSystemClient.DeleteDirectory(directoryPath);
            }
        }
    }
}
