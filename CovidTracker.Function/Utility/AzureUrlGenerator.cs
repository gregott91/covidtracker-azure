namespace CovidTracker.Function.Utility
{
    public class AzureUrlGenerator
    {
        public string GenerateBuildUrl(string organization, string project, string status, string result, string numToReturn)
        {
            return $"https://dev.azure.com/{organization}/{project}/_apis/build/builds?$top={numToReturn}&statusFilter={status}&resultFilter={result}&api-version=5.0";
        }

        public string GeneratePackageUrl(string organization, string project, string buildID, string artifactName)
        {
            return $"https://dev.azure.com/{organization}/{project}/_apis/build/builds/{buildID}/artifacts?artifactName={artifactName}&api-version=5.0";
        }
    }
}
