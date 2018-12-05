using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreSample
{
    public static class ConfigSample
    {
        public static async Task AppSettings(HttpContext context, IConfigurationRoot config) {
            string settings=config["AppSettings:SiteName"];
            await context.Response.WriteAsync(settings.Div());
        }

        public static async Task ReadDatabaseConnection(HttpContext context, IConfigurationRoot config)
        {
            string settings = config["Data:ConnectionStrings:DefaultConnection"];
            await context.Response.WriteAsync(settings.Div());
        }
    }
}
