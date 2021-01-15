using System;
using System.Threading.Tasks;
using CovidTracker.Azure.Function.Clients;
using CovidTracker.Azure.Function.Clients.Models;
using CovidTracker.Azure.Function.Logic;
using CovidTracker.Azure.Function.Utility;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CovidTracker.Azure.Function
{
    public static class TimerHtmlGenerator
    {
        [FunctionName("TimerHtmlGenerator")]
        public static void Run([TimerTrigger("0 * * * *")] TimerInfo timer, ILogger log)
        {
            try
            {
                RunAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to run function");
            }
        }

        private static async Task RunAsync()
        {
            AzurePackageCoordinator coordinator = new AzurePackageCoordinator(
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
            new ZipFileDownloader(),
            new PathUtility());

            await coordinator.DownloadPackageAsync(new AzureArtifactConfiguration()
            {
                Organzation = "gregoryott2345",
                Project = "go-covidtracker",
                Package = "covidtracker",
                Executable = "covidtracker"
            });
        }
    }
}
