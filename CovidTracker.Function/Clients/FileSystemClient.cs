using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CovidTracker.Function.Clients
{
    public class FileSystemClient
    {
        public string CreateRandomDirectory(string parentDirectory)
        {
            string directoryPath = Path.Combine(parentDirectory, Guid.NewGuid().ToString());
            Directory.CreateDirectory(directoryPath);

            return directoryPath;
        }

        public string CreateRandomFile(string parentDirectory)
        {
            string fileName = Path.Combine(parentDirectory, Guid.NewGuid().ToString());
            return fileName;
        }

        public void WriteStreamToFile(string fileName, Stream stream)
        {
            using FileStream fileStream = File.Create(fileName);

            stream.Seek(0, SeekOrigin.Begin);
            stream.CopyTo(fileStream);
        }
    }
}
