using CovidTracker.Function.Clients;
using CovidTracker.Function.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class StaticSiteGenerator
    {
        private CommandClient _exeClient;
        private FileSystemClient _fileSystemClient;

        public StaticSiteGenerator(CommandClient exeClient, FileSystemClient fileSystemClient)
        {
            _exeClient = exeClient;
            _fileSystemClient = fileSystemClient;
        }

        public async Task<string> GenerateSiteAsync(
            string exeFile,
            string directoryPath,
            string outputFileName,
            ILoggingClient logger)
        {
            string outputFile = _fileSystemClient.GetFile(directoryPath, outputFileName);
            await _exeClient.RunAsync(new ProcessConfig()
            {
                Command = exeFile,
                Arguments = outputFile
            }, logger);

            return outputFile;
        }
    }
}
