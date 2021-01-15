using CovidTracker.Azure.Function.Clients;
using CovidTracker.Azure.Function.Clients.Models;
using CovidTracker.Azure.Function.Models;
using CovidTracker.Azure.Function.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Azure.Function.Logic
{
    public class AzurePackageCoordinator
    {
        private AzurePackageStreamer _packageStreamer;
        private ZipFileDownloader _downloader;
        private PathUtility _pathUtil;

        public AzurePackageCoordinator(
            AzurePackageStreamer packageStreamer,
            ZipFileDownloader fileDownloader,
            PathUtility pathUtil)
        {
            _packageStreamer = packageStreamer;
            _downloader = fileDownloader;
            _pathUtil = pathUtil;
        }

        public async Task<string> DownloadPackageAsync(AzureArtifactConfiguration config)
        {
            string directoryPath = config.Executable;

            using Stream stream = await _packageStreamer.StreamLatestPackageAsync(config.Organzation, config.Project, config.Package);

            string path = DownloadToPath(stream, directoryPath);

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

        private string DownloadToPath(Stream stream, string directoryPath)
        {
            _downloader.DownloadAndExtract(stream, directoryPath);

            return Path.GetFullPath(directoryPath);
        }
    }
}
