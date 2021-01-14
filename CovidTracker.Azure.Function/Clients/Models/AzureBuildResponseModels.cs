using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Azure.Function.Clients.Models
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
