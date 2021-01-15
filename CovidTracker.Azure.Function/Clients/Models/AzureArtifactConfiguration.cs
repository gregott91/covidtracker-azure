﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Azure.Function.Clients.Models
{
    public class AzureArtifactConfiguration
    {
        public string Organzation { get; set; }

        public string Project { get; set; }

        public string Package { get; set; }

        public string Executable { get; set; }
    }
}
