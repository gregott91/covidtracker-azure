﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CovidTracker.Interop.Clients
{
    public class FileSystemClient
    {
        public string CreateRandomDirectory(string parentDirectory)
        {
            return CreateDirectory(parentDirectory, Guid.NewGuid().ToString());
        }

        public string CreateDirectory(string parentDirectory, string directoryName)
        {
            string directoryPath = Path.Combine(parentDirectory, directoryName);
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

        public void CopyFile(string sourceDirectory, string destinationDirectory, string fileName)
        {
            File.Copy(Path.Combine(sourceDirectory, fileName), Path.Combine(destinationDirectory, fileName), true);
        }

        public void DeleteDirectory(string directory)
        {
            Directory.Delete(directory, true);
        }

        public string GetFile(string parentDirectory, string fileName)
        {
            return Path.Combine(parentDirectory, fileName);
        }

        public string GetDirectory(string parentDirectory, string directory)
        {
            return Path.Combine(parentDirectory, directory);
        }

        public string GetFileNameFromPath(string filePath)
        {
            return Path.GetFileName(filePath);
        }

        public string GetFileDirectory(string filePath)
        {
            return Path.GetDirectoryName(filePath);
        }
    }
}
