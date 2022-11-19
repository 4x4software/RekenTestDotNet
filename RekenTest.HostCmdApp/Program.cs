using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RekenTest.Common.Implementers;
using System.Reflection;
using RekenTest.HostCmdApp;
using RekenTest.Common.Interfaces;
using RekenTest.Database.Interfaces;
using RekenTest.Database.Implementers;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddScoped<IProblemValueFactory, ProblemValueFactory>()
            .AddScoped<IProblem, Problem>()
            .AddScoped<IDatastore, Datastore>()
            .AddHostedService<GenerateAnswerService>()
            .AddOptions<Settings>().Bind(hostContext.Configuration);
    });

await builder
    .RunConsoleAsync();
