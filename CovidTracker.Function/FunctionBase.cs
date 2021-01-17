﻿using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Models;
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
        private readonly MasterGenerator _generator;

        public FunctionBase(MasterGenerator generator)
        {
            this._generator = generator;
        }

        protected async Task RunAsync(ILogger logger)
        {
            await _generator.HandleGenerationAsync(
                new AzureArtifactConfiguration()
                {
                    Organzation = "gregoryott2345",
                    Project = "go-covidtracker",
                    Package = "covidtracker",
                    Executable = "covidtracker",
                    DefinitionID = "1"
                },
                new GitConfig()
                {
                    CloneUrl = "https://github.com/gregott91/CovidTracker.git",
                    RepoName = "CovidTracker"
                },
                "index.html",
                "D:\\local\\temp",
                new FunctionLoggingClient(logger));
        }
    }
}
