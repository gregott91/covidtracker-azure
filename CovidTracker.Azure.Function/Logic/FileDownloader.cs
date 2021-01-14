using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CovidTracker.Azure.Function.Logic
{
    public class FileDownloader
    {
        public void DownloadFileFromStream(Stream stream, string fileName)
        {
            using FileStream fileStream = File.Create(fileName);

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }
    }
}
