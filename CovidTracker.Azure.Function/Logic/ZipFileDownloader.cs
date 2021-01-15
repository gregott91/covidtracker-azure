using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CovidTracker.Azure.Function.Logic
{
    public class ZipFileDownloader
    {
        public void DownloadAndExtract(Stream stream, string extractDirectory)
        {
            string fileName = Guid.NewGuid().ToString();

            DownloadFileFromStream(stream, fileName);
            ExtractToDirectory(fileName, extractDirectory);
        }

        private void DownloadFileFromStream(Stream stream, string fileName)
        {
            using FileStream fileStream = File.Create(fileName);

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }

        private void ExtractToDirectory(string zipFileName, string extractDirectory)
        {
            ZipFile.ExtractToDirectory(zipFileName, extractDirectory, true);
        }
    }
}
