using MCRP.MSF.Core.Repositories; 
using MCRP.MultiTenancy.ORM.Repositories;
using MCRP.ServiceHost.Core.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.EntityFramework.Oracle;
using System;
using System.Collections.Generic;
using System.Text;
using Mcrp.shujuzt.customer.ORM;
using Mcrp.shujuzt.customer.ORM.Models;

namespace Mcrp.shujuzt.customer.API
{
   public  class Startup:  IMCRPStartup
    {

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            base.Configure(app, env, configuration);
        }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IReadOnlyBasicRepository<SampleModel, string>, EfCoreRepository<DefaultDbContext, SampleModel, string>>();
            base.ConfigureServices(services, configuration);
            services.AddCustomDbContext<DefaultDbContext>(opt =>
            {
                opt.UseEfCoreOracle(configuration.GetConnectionString("default"));
                opt.EnableSensitiveDataLogging();
            });
        }
    }
}
