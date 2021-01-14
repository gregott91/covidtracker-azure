using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CovidTracker.Azure.Function
{
    public static class TimerHtmlGenerator
    {
        [FunctionName("TimerHtmlGenerator")]
        public static void Run([TimerTrigger("0 * * * *")]TimerInfo timer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
