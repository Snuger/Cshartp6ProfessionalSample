using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Serilog;

namespace serilogSamples
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

            services.Configure<KestrelServerOptions>(opt=> {
                opt.AllowSynchronousIO=true;
            });


            //services.Configure<RequestLoggingOptions>(opt =>
            //{
            //    opt.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
            //     {
            //         diagnosticContext.Set("RemoteIpAddress", httpContext.Connection.RemoteIpAddress.MapToIPv4());
            //     };
            //});

            //services.AddMvc(opt=> {
            //    opt.Filters.Add(typeof(SerilogLoggingActionFilter));
            //});
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //app.Use(next => context =>
            //{
            //    context.Request.EnableBuffering();
            //    return next(context);
            //});

            app.UseSerilogRequestLogging();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
