using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Models;
using CovidTracker.Function.Utility;
using CovidTracker.Git.Clients;
using CovidTracker.Git.Models;
using CovidTracker.Interop.Clients;
using CovidTracker.Logging;
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
            var generator = new StaticSiteUpdater(
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
                        new PathUtility()),
                    new StaticSiteGenerator(new CommandClient(), new FileSystemClient()),
                    new GitPagesUploader(new FileSystemClient()),
                    new FileSystemClient());

            await generator.GenerateAndUploadAsync(
                new AzureArtifactConfiguration()
                {
                    Organzation = "gregoryott2345",
                    Project = "go-covidtracker",
                    Package = "covidtracker",
                    Executable = "covidtracker",
                    DefinitionID = "1"
                },
                new GitSessionConfig()
                {
                    RepoName = "CovidTracker",
                    RepoGitInstallation = "github",
                    RepoOrganization = "gregott91",
                    Authenticate = false,
                },
                "index.html",
                "D:\\temp",
                new ConsoleLoggingClient());
        }
    }
}