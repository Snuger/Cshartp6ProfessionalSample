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
            //var host = new HostBuilder()              
            //    .ConfigureHostConfiguration(set =>
            //    {
            //        set.SetBasePath(Directory.GetCurrentDirectory());
            //        set.AddJsonFile("hostsettings.json", optional: true);
            //        set.AddCommandLine(args);
            //    })
            //    .ConfigureServices((context, services) => {                  
            //        services.AddHostedService<LifetimeEventsHostedService>();
            //    })
            //    .ConfigureLogging((hostContext, configLogging) =>
            //    {
            //        //configLogging.AddConsole();
            //        // configLogging.AddDebug();
            //    })
            //    .UseConsoleLifetime()
            //    .Build();
            // await  host.RunAsync();      

            var host = new HostBuilder()
                .ConfigureServices((context,service)=> {
                    service.Configure<HostOptions>(opt =>
                    {
                        opt.ShutdownTimeout = System.TimeSpan.FromSeconds(100);
                    });
                })
                .ConfigureAppConfiguration((context, app) => {
                   
                })
                .ConfigureLogging((context, logger) => {
                   
                })
                .Build();
            using (host)
            {
                await host.StartAsync();
                await host.WaitForShutdownAsync();
            }
       
        }
    }
}
