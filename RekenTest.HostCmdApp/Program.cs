using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RekenTest.Common.Implementers;
using System.IO;
using System.Reflection;
using RekenTest.HostCmdApp;
using RekenTest.Common.Interfaces;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddScoped<IProblemValueFactory, ProblemValueFactory>()
            .AddScoped<IProblem, Problem>()
            .AddHostedService<GenerateAnswerService>()
            .AddOptions<Settings>().Bind(hostContext.Configuration);
    });

await builder
    .RunConsoleAsync();
