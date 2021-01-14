using CovidTracker.Azure.Function.Clients.Models;
using CovidTracker.Azure.Function.Utility;
using System.Threading.Tasks;

namespace CovidTracker.Azure.Function.Clients
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

        public async Task<AzurePackage> GetBuildPackageAsync(string organization, string project, string buildID, string artifactName)
        {
            string url = _urlGenerator.GeneratePackageUrl(organization, project, buildID, artifactName);
            using var stream = await _httpClient.GetAsync(url);

            return await _jsonClient.DeserializeAsync<AzurePackage>(stream);
        }
    }
}
