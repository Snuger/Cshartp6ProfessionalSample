using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JmeterIntegrated
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)           
            .ConfigureAppConfiguration((Context, config) =>
            {
                config.AddJsonFile("hosting.json", optional: true);

            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
               
                webBuilder.UseUrls("http://*:8080");
                webBuilder.UseStartup<Startup>();
            });
    }
}
