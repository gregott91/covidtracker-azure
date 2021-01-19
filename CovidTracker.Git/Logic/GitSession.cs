using CovidTracker.Git.Clients;
using CovidTracker.Git.Models;
using CovidTracker.Git.Utility;
using CovidTracker.Interop.Clients;
using CovidTracker.Interop.Models;
using CovidTracker.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CovidTracker.Git.Logic
{
    public class GitSession : IDisposable
    {
        private bool _initialized = false;
        private GitSessionConfig _config;
        private string _repoPath;

        private GitClient _client;
        private FileSystemClient _fileSystem;
        private ILoggingClient _logger;

        public GitSession(
            GitClient client,
            FileSystemClient fileSystem,
            ILoggingClient logger)
        {
            _client = client;
            _fileSystem = fileSystem;
            _logger = logger;
        }

        public async Task InitializeAsync(GitSessionConfig config, string cloneLocation)
        {
            if (_initialized)
            {
                throw new InvalidOperationException("Attempted to initialize an already initialized git session");
            }

            _config = config;
            _repoPath = _fileSystem.GetDirectory(cloneLocation, _config.RepoName);

            if (config.Authenticate)
            {
                string authenticatedUrl = GitUrlUtility.GetAuthenticatedRepoUrl(
                    _config.RepoGitInstallation,
                    _config.RepoOrganization,
                    _config.RepoName,
                    _config.Username,
                    _config.Password);

                await _client.CloneAsync(
                    cloneLocation,
                    authenticatedUrl,
                    _logger);

                await _client.SetOrigin(_repoPath, authenticatedUrl, _logger);
            }
            else
            {
                await _client.CloneAsync(
                    cloneLocation,
                    GitUrlUtility.GetBasicRepoUrl(_config.RepoGitInstallation, _config.RepoOrganization, _config.RepoName),
                    _logger);
            }

            _initialized = true;
        }

        public async Task StageAsync(string fileName)
        {
            Validate();

            await _client.StageAsync(_repoPath, fileName, _logger);
        }

        public async Task SafeCommitAsync(string commitMessage)
        {
            Validate();

            try
            {
                await _client.CommitAsync(_repoPath, commitMessage, _logger);
            }
            catch (CommandException ex)
            {
                if (ex.ExitCode == 128)
                {
                    _logger.LogWarning("Unable to commit - exit code is 128. Likely no changes to commit.");
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task SafePushAsync()
        {
            try
            {
                await _client.PushAsync(_repoPath, _logger);
            }
            catch (CommandException ex)
            {
                if (ex.ExitCode == 1)
                {
                    _logger.LogWarning("Unable to push - exit code is 1. Likely no changes to push.");
                }
                else
                {
                    throw;
                }
            }
        }

        public string GetRepoLocation()
        {
            Validate();

            return _repoPath;
        }

        public void Dispose()
        {
            try
            {
                _fileSystem.DeleteDirectory(_repoPath);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Unable to delete local copy of repo");
            }

            _initialized = false;
        }

        private void Validate()
        {
            if (!_initialized)
            {
                throw new InvalidOperationException("Attempted to use an uninitialized git session");
            }

            if (_config == null)
            {
                throw new InvalidOperationException("Initialized git session has no config");
            }
        }
    }
}
