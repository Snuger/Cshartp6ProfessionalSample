using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GenericHostSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = CreateHostBuilder(args).Build()) {
                host.Run();
                host.Services.GetRequiredService("");


            }
           
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureServices((context, service) =>
            {                
                service.Configure<MyOptions>(opt =>
                {
                    opt.Optinos1 = "来来来，我们一起来";
                    opt.Options2 = 100;
                });

            });
    }
}
