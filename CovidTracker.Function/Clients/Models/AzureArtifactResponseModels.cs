namespace CovidTracker.Function.Clients.Models
{
    public class AzurePackageResource
    {
        public string DownloadUrl { get; set; }
    }

    public class AzurePackage
    {
        public int ID { get; set; }

        public AzurePackageResource Resource { get; set; }
    }
}
