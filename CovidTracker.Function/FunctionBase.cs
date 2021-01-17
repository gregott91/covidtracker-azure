using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Function
{
    public abstract class FunctionBase
    {
        private readonly AzurePackageCoordinator _packageCoordinator;

        public FunctionBase(AzurePackageCoordinator packageCoordinator)
        {
            this._packageCoordinator = packageCoordinator;
        }

        protected async Task RunAsync(ILogger logger)
        {
            await _packageCoordinator.DownloadPackageAsync(new AzureArtifactConfiguration()
            {
                Organzation = "gregoryott2345",
                Project = "go-covidtracker",
                Package = "covidtracker",
                Executable = "covidtracker",
                DefinitionID = "1"
            },
            "D:\\local\\temp",
            new FunctionLoggingClient(logger));
        }
    }
}
