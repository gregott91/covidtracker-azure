using System;
using System.Threading.Tasks;
using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Utility;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace CovidTracker.Function
{
    public class SiteGenerator : FunctionBase
    {
        public SiteGenerator(MasterGenerator generator) : base(generator)
        {}

        [FunctionName("SiteGenerator")]
        public void Run([TimerTrigger("0 * * * *")] TimerInfo timer, ILogger log)
        {
            try
            {
                RunAsync(log).ConfigureAwait(false).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                log.LogError(ex, "Unable to run timed function");
            }
        }
    }
}
