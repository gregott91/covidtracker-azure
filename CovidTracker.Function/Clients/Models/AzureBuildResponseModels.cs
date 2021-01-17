using System.Collections.Generic;

namespace CovidTracker.Function.Clients.Models
{
    public class AzureBuild
    {
        public int ID { get; set; }
    }

    public class AzureBuilds
    {
        public int Count { get; set; }

        public IEnumerable<AzureBuild> Value { get; set; }
    }
}
