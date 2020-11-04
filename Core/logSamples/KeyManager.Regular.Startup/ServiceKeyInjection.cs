using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;


[assembly: HostingStartup(typeof(KeyManager.Regular.Startup.ServiceKeyInjection))]
namespace KeyManager.Regular.Startup
{   
    public class ServiceKeyInjection : IHostingStartup
    {
     
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((hostBuildContext, config) =>
            {
                var dict = new Dictionary<string, string>
                {
                    {"DevAccount_FromLibrary", "DEV_1111111-1111"},
                    {"ProdAccount_FromLibrary", "PROD_2222222-2222"}
                };

                config.AddInMemoryCollection(dict);
            });
        }
    }
}
