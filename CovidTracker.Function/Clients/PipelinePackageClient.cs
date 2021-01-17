using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Utility;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class PipelinePackageClient
    {
        private AzureUrlGenerator _urlGenerator;
        private HttpClientWrapper _httpClient;
        private JsonClient _jsonClient;

        public PipelinePackageClient(
            AzureUrlGenerator urlGenerator,
            HttpClientWrapper httpClient,
            JsonClient jsonClient)
        {
            _urlGenerator = urlGenerator;
            _httpClient = httpClient;
            _jsonClient = jsonClient;
        }

        public async Task<AzurePackage> GetBuildPackageAsync(string organization, string project, string buildID, string artifactName, ILoggingClient logger)
        {
            string url = _urlGenerator.GeneratePackageUrl(organization, project, buildID, artifactName);
            using var stream = await _httpClient.GetAsync(url, logger);

            return await _jsonClient.DeserializeAsync<AzurePackage>(stream);
        }
    }
}
