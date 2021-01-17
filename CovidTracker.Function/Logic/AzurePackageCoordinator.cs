using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Models;
using CovidTracker.Function.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class AzurePackageCoordinator
    {
        private AzurePackageStreamer _packageStreamer;
        private ZipFileDownloader _downloader;
        private PathUtility _pathUtil;
        private FileSystemClient _fileSystem;

        public AzurePackageCoordinator(
            AzurePackageStreamer packageStreamer,
            ZipFileDownloader fileDownloader,
            PathUtility pathUtil,
            FileSystemClient fileSystem)
        {
            _packageStreamer = packageStreamer;
            _downloader = fileDownloader;
            _pathUtil = pathUtil;
            _fileSystem = fileSystem;
        }

        public async Task<string> DownloadPackageAsync(AzureArtifactConfiguration config, string directoryPath, ILoggingClient logger)
        {
            logger.LogInfo($"Downloading to {directoryPath}");

            using Stream stream = await _packageStreamer.StreamLatestPackageAsync(config.Organzation, config.Project, config.Package, config.DefinitionID, logger);

            string path = DownloadToPath(stream, directoryPath, logger);

            IEnumerable<string> dirs = _pathUtil.GetDirectories(path);

            if (dirs.Count() > 1)
            {
                throw new CovidTrackerException($"Too many directories in the extracted package");
            }

            string packageFilePath = _pathUtil.FindFile(dirs.First(), config.Executable);

            if (string.IsNullOrEmpty(packageFilePath))
            {
                throw new CovidTrackerException($"Unable to find package file {config.Executable} in extractDirectory");
            }

            return packageFilePath;
        }

        private string DownloadToPath(Stream stream, string directoryPath, ILoggingClient logger)
        {
            _downloader.DownloadAndExtract(stream, directoryPath, logger);

            return Path.GetFullPath(directoryPath);
        }
    }
}
