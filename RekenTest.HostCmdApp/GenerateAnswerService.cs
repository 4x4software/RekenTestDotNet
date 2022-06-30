using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RekenTest.Common;
using RekenTest.Common.Implementers;
using RekenTest.Common.Interfaces;

namespace RekenTest.HostCmdApp
{
    internal class GenerateAnswerService : ConsoleWorkerService
    {
        private readonly Settings _settings;
        private readonly IProblem _problem;
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifeTime;

        private string TypeName => GetType().Name;

        public GenerateAnswerService(
            ILogger<GenerateAnswerService> logger,
            IOptions<Settings> settings,
            IProblem problem,
            IHostApplicationLifetime appLifetime): base(logger, appLifetime)
        {
            _logger = logger;
            _settings = settings.Value;
            _problem = problem;
            _appLifeTime = appLifetime;
        }

        protected override void DoWork()
        {
            if (_problem.ParseFromString(_settings.Problem))
                _logger.LogInformation(_problem.GetCorrectAnswer().ToString());
            else
                _logger.LogError("Invalid problem: " + _settings.Problem);
        }
    }
}