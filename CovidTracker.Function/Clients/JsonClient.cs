using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace CovidTracker.Function.Clients
{
    public class JsonClient
    {
        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return await JsonSerializer.DeserializeAsync<T>(stream, options);
        }
    }
}
