using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using MCRP.External.Jmeter.Utilities;

namespace MCRP.External.Jmeter
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

 
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<ProcessStartInfo>();
            services.Configure<JmeterOptions>(Configuration.GetSection("JmeterOptions"));            
            services.AddTransient<IJmeterPlanActuator,JmeterPlanActuator>();
            services.AddTransient<IJmxFileOperator,JmxFileOperator>();
           

            services.AddMvc();  
            services.AddSwaggerGen(c=>{
                    c.SwaggerDoc("v1",new OpenApiInfo(){Title="Jmeter集成Api",Version="1.0.0"});

            });         
            services.AddControllers();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Jmeter External Api");
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
      
          
        }
    }
}
