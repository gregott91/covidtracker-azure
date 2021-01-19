using CovidTracker.Function.Models;
using CovidTracker.Logging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class HttpClientWrapper
    {
        private static HttpClient _httpClient = new HttpClient();

        public async Task<Stream> GetAsync(string url, ILoggingClient logger)
        {
            logger.LogInfo($"Sending GET to {url}");

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStreamAsync();
            }
            else
            {
                throw new CovidTrackerException($"Unable to GET {url}, response code {response.StatusCode}");
            }
        }
    }
}
