using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RekenTest.Common.Interfaces;
using RekenTest.Database.Interfaces;

namespace RekenTest.HostCmdApp
{
    internal class GenerateAnswerService : ConsoleWorkerService
    {
        private readonly Settings _settings;
        private readonly IProblem _problem;
        private readonly IDatastore _datastore;
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifeTime;

        private string TypeName => GetType().Name;

        public GenerateAnswerService(
            ILogger<GenerateAnswerService> logger,
            IOptions<Settings> settings,
            IProblem problem,
            IDatastore datastore,
            IHostApplicationLifetime appLifetime): base(logger, appLifetime)
        {
            _logger = logger;
            _settings = settings.Value;
            _problem = problem;
            _datastore = datastore;
            _appLifeTime = appLifetime;
        }

        protected override void DoWork()
        {
            _logger.LogInformation($"Input value: {_settings.Problem}");
            if (_problem.ParseFromString(_settings.Problem))
            {
                _logger.LogInformation($"Answer: {_problem.GetCorrectAnswer()}");

                if (_datastore.StoreProblem(_problem))
                    _logger.LogInformation("Successfully stored in database");
                else
                    _logger.LogError("Error while storing problem in database");
            }
            else
                _logger.LogError("Invalid problem!");
        }
    }
}