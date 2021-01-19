using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Git.Models
{
    public class GitSessionConfig
    {
        public string Email { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string RepoGitInstallation { get; set; }

        public string RepoOrganization { get; set; }

        public string RepoName { get; set; }

        public bool Authenticate { get; set; }
    }
}
