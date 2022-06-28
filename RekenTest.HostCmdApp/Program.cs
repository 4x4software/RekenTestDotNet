using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RekenTest.Common.Implementers;
using System.IO;
using System.Reflection;
using RekenTest.HostCmdApp;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .UseContentRoot(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    .ConfigureServices((hostContext, services) =>
    {
        services
            .AddSingleton<Problem>()
            .AddHostedService<GenerateAnswerService>();
            //.AddOptions<Settings>().Bind(hostContext.Configuration);
    });

await builder
    .RunConsoleAsync();
