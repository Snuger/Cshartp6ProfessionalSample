using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MvcApplicationSample.Models;
using MvcApplicationSample.Services;

namespace MvcApplicationSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddScoped<EventsAndMenusContext>();
            services.AddScoped<ZoningContetxt>();
            //services.AddTransient<ISampleService, DefaultSampleService>();
            services.AddScoped<ISampleService, DefaultSampleService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            //app.UseMvcWithDefaultRoute();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "default",
            //        // template: "{controller=Home}/{action=Index}/{id?}",
            //        template: "{action}/{id?}",
            //        defaults:new { controller="Home",action="Index" });
                   
            //});

            app.UseMvc(routes=> {
                routes.MapRoute(name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}")
                    .MapRoute(
                    name:"language",
                    template:"{language}/{controller}/{action}/{id?}",
                    defaults:new {controller="Home",action="Index" },
                    constraints:new {language=@"(en)|(zh-cn)" });                  
            });
        }
    }
}
