using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Models;
using CovidTracker.Function.Utility;
using System;
using System.Threading.Tasks;

namespace CovidTracker.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsync()
        {
            var generator = new MasterGenerator(
                    new AzurePackageCoordinator(
                        new AzurePackageStreamer(
                            new PipelinePackageClient(
                                new AzureUrlGenerator(),
                                new HttpClientWrapper(),
                                new JsonClient()),
                            new PipelineBuildClient(
                                new AzureUrlGenerator(),
                                new HttpClientWrapper(),
                                new JsonClient()),
                            new HttpClientWrapper()),
                        new ZipFileDownloader(new FileSystemClient()),
                        new PathUtility(),
                        new FileSystemClient()),
                    new GitManager(new GitClient(new CommandClient())),
                    new CommandClient(),
                    new FileSystemClient());

            await generator.HandleGenerationAsync(
                new AzureArtifactConfiguration()
                {
                    Organzation = "gregoryott2345",
                    Project = "go-covidtracker",
                    Package = "covidtracker",
                    Executable = "covidtracker",
                    DefinitionID = "1"
                },
                new GitConfig()
                {
                    CloneUrl = "https://github.com/gregott91/CovidTracker.git",
                    RepoName = "CovidTracker"
                },
                "index.html",
                        "D:\\temp",
                new ConsoleLoggingClient());
        }
    }
}
