﻿using CovidTracker.Function.Clients;
using CovidTracker.Function.Clients.Models;
using CovidTracker.Function.Logic;
using CovidTracker.Function.Models;
using CovidTracker.Git.Models;
using CovidTracker.Logging;
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
        private readonly StaticSiteUpdater _generator;

        public FunctionBase(StaticSiteUpdater generator)
        {
            this._generator = generator;
        }

        protected async Task RunAsync(ILogger logger)
        {
            await _generator.GenerateAndUploadAsync(
                new AzureArtifactConfiguration()
                {
                    Organzation = "gregoryott2345",
                    Project = "go-covidtracker",
                    Package = "covidtracker",
                    Executable = "covidtracker",
                    DefinitionID = "1"
                },
                new GitSessionConfig()
                {
                    RepoName = "CovidTracker",
                    Username = "gregott91",
                    Email = "gregoryott2345@gmail.com",
                    Password = Environment.GetEnvironmentVariable("githubtoken", EnvironmentVariableTarget.Process),
                    RepoGitInstallation = "github",
                    RepoOrganization = "gregott91",
                    Authenticate = true,
                },
                "coviddata.json",
                "D:\\local\\temp",
                new FunctionLoggingClient(logger));
        }
    }
}
