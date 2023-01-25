using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RekenTest.HostCmdApp
{
    public abstract class ConsoleWorkerService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _appLifeTime;
        private static bool _isRunning;
        private static readonly object Lock = new();

        private string TypeName => GetType().Name;

        /// <summary>
        /// Overwrite this method when a sync implementation of the console application is used
        /// </summary>
        protected virtual void DoWork() => throw new NotImplementedException();

        protected ConsoleWorkerService(ILogger logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _appLifeTime = appLifetime;
        }

        protected sealed override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Make sure only one worker exists since the application will terminate once work is finished.
            lock (Lock)
                if (!_isRunning)
                    _isRunning = true;
                else
                    throw new ApplicationException($"{GetType().BaseType?.Name} already running, Application aborted");

            // Register the desired worker
            _appLifeTime.ApplicationStarted.Register(() => Task.Run(() => RunSync()));

            return Task.CompletedTask;
        }

        private void RunSync()
        {
            try
            {
                _logger.LogDebug("Starting: '{typeName}'", TypeName);
                DoWork();
                _logger.LogDebug("Finished: '{typeName}'", TypeName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception in: '{typeName}'", TypeName);
                Environment.ExitCode = 1;
            }
            finally
            {
                _appLifeTime.StopApplication();
            }
        }
    }
}