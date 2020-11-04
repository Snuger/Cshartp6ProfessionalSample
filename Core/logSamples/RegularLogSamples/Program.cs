using System;
using System.IO;
using System.Numerics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using RegularLogSamples.Services;
using Microsoft.Extensions.Logging;

namespace RegularLogSamples
{
    class Program
    {     
        
        static void Main(string[] args)
        {        
           var host= CreateHostBuilder(args).Build();
           var logger= host.Services.GetRequiredService<ILogger<Program>>();         
           host.Run();
           logger.LogInformation($"启动成功后输出");
        } 
        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
         .ConfigureAppConfiguration((hostContext,appConfig)=> {
             appConfig.SetBasePath(Directory.GetCurrentDirectory());
             var env = hostContext.HostingEnvironment;
             appConfig.AddJsonFile("appsetting.json",optional:true);
             appConfig.AddJsonFile($"appsetting.{env.EnvironmentName}.json",optional: true);
             appConfig.AddEnvironmentVariables();
         })
        .ConfigureServices((hostContext, services) =>
        {            
            services.AddHostedService<ExampleHostedService>();
            services.AddHostedService<ConsumeScopedServiceHostedService>();
            services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
            services.AddDbContext<ApplicationDbContext>(opt=> {         
                opt.UseSqlite("Data Source=DB/blogging.db");
            });
            services.AddLogging();
        });
    }
}
