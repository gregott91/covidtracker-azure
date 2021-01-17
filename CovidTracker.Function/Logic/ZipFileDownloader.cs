using CovidTracker.Function.Clients;
using System;
using System.IO;
using System.IO.Compression;

namespace CovidTracker.Function.Logic
{
    public class ZipFileDownloader
    {
        private FileSystemClient _fileSystem;

        public ZipFileDownloader(FileSystemClient fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void DownloadAndExtract(Stream stream, string extractDirectory, ILoggingClient logger)
        {
            string fileName = _fileSystem.CreateRandomFile(extractDirectory);

            logger.LogInfo($"Download filename: {fileName}");

            _fileSystem.WriteStreamToFile(fileName, stream);
            ExtractToDirectory(fileName, extractDirectory);
        }

        private void ExtractToDirectory(string zipFileName, string extractDirectory)
        {
            ZipFile.ExtractToDirectory(zipFileName, extractDirectory, true);
        }
    }
}
