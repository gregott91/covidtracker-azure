using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CovidTracker.Azure.Function.Clients
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
