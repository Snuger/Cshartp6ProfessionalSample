using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GenericHostSample
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
            .ConfigureLogging((context, logger) =>
            { 
                logger.AddConsole();
                logger.AddDebug();
            })
            .ConfigureHostConfiguration(config =>
            {
                config.AddEnvironmentVariables();
            })
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                config.AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                config.AddCommandLine(args);
            })
            .ConfigureServices((context, services) =>
            {              
                services.AddLogging();
                services.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
                services.AddSingleton<MonitorLoop>();
                services.AddHostedService<TimedHostedService>();
            })   
            .UseEnvironment(EnvironmentName.Development)
            .UseConsoleLifetime()
            .Build();

            using (host)
            {
                await host.StartAsync();

                var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
                monitorLoop.StartMonitorLoop();

                
                await host.WaitForShutdownAsync();

            }
                
        }
    }
}
