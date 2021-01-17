using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CovidTracker.Function.Logic;

namespace CovidTracker.Function
{
    public class SiteGeneratorHttpTrigger : FunctionBase
    {
        public SiteGeneratorHttpTrigger(AzurePackageCoordinator packageCoordinator) : base(packageCoordinator)
        { }

        [FunctionName("SiteGeneratorHttpTrigger")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                await RunAsync(log);
                return new OkResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to run HTTP trigger function");
                return new StatusCodeResult(500);
            }
        }
    }
}
