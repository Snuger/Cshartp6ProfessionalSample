using Template.API.Services;
using MCRP.ServiceHost.Core.Abstractions;
using Template.ORM;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Starter.EntityFramework.Postgresql;
using System.Data;
using Template.Service.IServices;
using Template.Service.Services;

namespace Template.Host
{
    public class Startup : IMCRPStartup
    {
        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            base.Configure(app, env, configuration);
            app.UseCors("allowAll");
            app.UseFastReport(op=>op.RouteBasePath= "mcrp-data-shujutjzx");

        }

        /// <summary>
        /// ������
        /// </summary>
        public IConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            base.ConfigureServices(services, configuration);
            services.AddControllersWithViews();
            services.AddCustomIntegrations();        
            services.AddCors((ac) =>
            {
                ac.AddPolicy("allowAll", (policy) =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });  
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<ISampleMSFDapperSevice, SampleDapperSevice>();
            services.AddScoped<ISampleMSFEFCoreSevice, SampleMSFEFCoreSevice>();
            services.AddCustomDbContext<DataDbContext>(option =>
            {
                option.UseEntityFrameworkNpgsql(configuration.GetConnectionString("default"));
            });
            services.AddScoped<IDbConnection>(sp => sp.GetRequiredService<DataDbContext>().Database.GetDbConnection());
        }
    }

}
