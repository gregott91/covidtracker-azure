using CovidTracker.Azure.Function.Clients.Models;
using CovidTracker.Azure.Function.Utility;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Azure.Function.Clients
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

        public async Task<AzureBuilds> GetBuildsAsync(string organization, string project, string status, int numToReturn)
        {
            string url = _urlGenerator.GenerateBuildUrl(organization, project, status, numToReturn.ToString());
            using var stream = await _httpClient.GetAsync(url);

            return await _jsonClient.DeserializeAsync<AzureBuilds>(stream);
        }
    }
}
