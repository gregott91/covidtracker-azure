using CovidTracker.Function.Clients;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Utility;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(CovidTracker.Function.Startup))]
namespace CovidTracker.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddTransient((s) =>
            {
                return new StaticSiteUpdater(
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
                    new StaticSiteGenerator(new CommandClient(), new FileSystemClient()),
                    new GitPagesUploader(
                        new GitManager(
                            new GitClient(new CommandClient()
                        )),
                        new FileSystemClient()),
                    new FileSystemClient());
            });
        }
    }
}