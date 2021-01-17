using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Utility;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class PipelineBuildClient
    {
        private AzureUrlGenerator _urlGenerator;
        private HttpClientWrapper _httpClient;
        private JsonClient _jsonClient;

        public PipelineBuildClient(
            AzureUrlGenerator urlGenerator,
            HttpClientWrapper httpClient,
            JsonClient jsonClient)
        {
            _urlGenerator = urlGenerator;
            _httpClient = httpClient;
            _jsonClient = jsonClient;
        }

        public async Task<AzureBuilds> GetBuildsAsync(string organization, string project, string status, string result, string definitionID, int numToReturn, ILoggingClient logger)
        {
            string url = _urlGenerator.GenerateBuildUrl(organization, project, status, result, definitionID, numToReturn.ToString());
            using var stream = await _httpClient.GetAsync(url, logger);

            return await _jsonClient.DeserializeAsync<AzureBuilds>(stream);
        }
    }
}
