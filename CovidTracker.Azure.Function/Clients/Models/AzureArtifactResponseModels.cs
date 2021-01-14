using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Azure.Function.Clients.Models
{
    public class AzurePackage
    {
        public int ID { get; set; }

        public string DownloadUrl { get; set; }
    }
}
