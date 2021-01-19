using System;
using System.Collections.Generic;
using System.Text;

namespace CovidTracker.Git.Utility
{
    public static class GitUrlUtility
    {
        public static string GetAuthenticatedRepoUrl(
            string repoGitInstallation,
            string repoOrganization,
            string repoName,
            string username,
            string password)
        {
            return $"https://{username}:{password}@{repoGitInstallation}.com/{repoOrganization}/{repoName}.git";
        }

        public static string GetBasicRepoUrl(
            string repoGitInstallation,
            string repoOrganization,
            string repoName)
        {
            return $"https://{repoGitInstallation}.com/{repoOrganization}/{repoName}.git";
        }
    }
}
