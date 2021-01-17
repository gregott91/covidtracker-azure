using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CovidTracker.Function.Logic
{
    public class AzurePackageStreamer
    {
        private PipelinePackageClient _packageClient;
        private PipelineBuildClient _buildClient;
        private HttpClientWrapper _httpClient;

        public AzurePackageStreamer(
            PipelinePackageClient packageClient,
            PipelineBuildClient buildClient,
            HttpClientWrapper httpClient)
        {
            _packageClient = packageClient;
            _buildClient = buildClient;
            _httpClient = httpClient;
        }

        public async Task<Stream> StreamLatestPackageAsync(string organization, string project, string artifactName)
        {
            AzureBuild build = await GetLatestBuildAsync(organization, project);
            AzurePackage package = await _packageClient.GetBuildPackageAsync(organization, project, build.ID.ToString(), artifactName);

            return await _httpClient.GetAsync(package.Resource.DownloadUrl);
        }

        private async Task<AzureBuild> GetLatestBuildAsync(string organization, string project)
        {
            AzureBuilds builds = await _buildClient.GetBuildsAsync(organization, project, "completed", "succeeded", 1);

            if (builds.Count == 0)
            {
                throw new CovidTrackerException($"No builds returned for project {project} in organization {organization}");
            }

            return builds.Value.First();
        }
    }
}
