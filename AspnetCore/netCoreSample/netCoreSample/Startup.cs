using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using netCoreSample.Dao;
using netCoreSample.Services;
using netCoreSample.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace netCoreSample
{

    public class Startup
    {
        public Startup(IHostingEnvironment env){
            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(env.ContentRootPath)
            //   .AddJsonFile("appsettings.json")
            //   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment()) {
                builder.AddUserSecrets<Startup>();
            }
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ISampleServices, DefaultSampleService>();
            services.AddTransient<HomeController>();
            services.AddMemoryCache();
           // services.AddCache();
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(10);

            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //{

        //    if (env.IsDevelopment())
        //    {
        //        app.UseDeveloperExceptionPage();
        //    }

        //    app.Run(async (context) =>
        //    {
        //        await context.Response.WriteAsync("Hello World!");
        //    });
        //}

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();
            app.UseStaticFiles();
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}         

            app.UseForwardedHeaders();

            //app.UseSession();

            app.Run(async (context) =>
            {
                var sb = new StringBuilder();
                sb.Append(HtmlHelper.DocType() + HtmlHelper.HtmlStart() + HtmlHelper.Head() + HtmlHelper.BodyStart());
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/hello.html"">Static Files</a> - requires UseStaticFiles</li>");
                sb.Append(@"<li><a href=""/RequestAndResponse"">Request and Response</a>");
                sb.Append("<li>Request and Response 2");
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/header"">Header</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/add?x=3&y=4"">Add</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=sample"">Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=<h1>sample</h1>"">HTML Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/content?data=<script>alert('hacker');</script>"">Bad Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/encoded?data=<h1>sample</h1>"">Encoded content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/encoded?data=<script>alert('hacker');</script>"">Encoded bad Content</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/form"">Form</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/writecookie"">Write cookie</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/readcookie"">Read cookie</a></li>");
                sb.Append(@"<li><a href=""/RequestAndResponse2/json"">JSON</a></li>");
                sb.Append("</ul>");
                sb.Append("</li>");
                sb.Append(@"<li><a href=""/home2"">Home Controller with dependency injection</a></li>");
                sb.Append(@"<li><a href=""/session"">Session</a></li>");
                sb.Append("<li>Configuration");
                sb.Append("<ul>");
                sb.Append(@"<li><a href=""/configuration/appsettings"">Appsettings</a></li>");
                sb.Append(@"<li><a href=""/configuration/database"">Database</a></li>");
                sb.Append(@"<li><a href=""/configuration/secret"">Secrets  </a></li>");
                sb.Append("</ul>");
                sb.Append("</li>");
                sb.Append("</ul>");
                sb.Append(HtmlHelper.BodyEnd() + HtmlHelper.HtmlEnd());
                await context.Response.WriteAsync(sb.ToString());
            });




            //app.Run(async (context) =>
            //{

            //    if (context.Request.Path.Value.ToLower() == "/home")
            //    {
            //        HomeController controller = app.ApplicationServices.GetService<HomeController>();
            //        await controller.Index(context);
            //        //context.Response.StatusCode = statusCode;
            //        //return;
            //    }
            //});


            //app.Map("/home2",homeController=> {
            //    homeController.Run(async context =>
            //    {
            //        HomeController controller = app.ApplicationServices.GetService<HomeController>();
            //        await controller.Index(context);
            //    });
            //});


            PathString remaining;
            app.MapWhen(context =>
                context.Request.Path.StartsWithSegments("/configuration", out remaining),
                configApp =>
                {
                    configApp.Run(async context =>
                    {
                        if (remaining.StartsWithSegments("/appsettings")) {
                            await ConfigSample.AppSettings(context, Configuration);
                        }
                        if (remaining.StartsWithSegments("/database"))
                        {
                            await ConfigSample.ReadDatabaseConnection(context, Configuration);
                        }
                    });
                }
            );

            app.Map("/configuration",configApp=>{
                configApp.Run(async context=> {
                    await ConfigSample.AppSettings(context,Configuration);
                });
            });


            app.Map("/getRequest",requestApp=> {
                requestApp.Run( async context=> {
                    await context.Response.WriteAsync(RequestAndResponseSample.GetRequestInformation(context.Request));
                });
            });

            app.Map("/getHeader",headerApp=> {
                headerApp.Run(async context=> {
                    await context.Response.WriteAsync(RequestAndResponseSample.GetHeaderInfomation(context.Request));
                });
            });

            app.Map("/form", formApp =>
            {
                formApp.Run(async context=> {
                    string result = RequestAndResponseSample.GetForm(context.Request);
                    await context.Response.WriteAsync(HtmlHelper.DocType()+HtmlHelper.HtmlStart()+HtmlHelper.BodyStart());
                    await context.Response.WriteAsync(result);
                    await context.Response.WriteAsync(HtmlHelper.BodyEnd() + HtmlHelper.HtmlEnd());
                });

            });

            app.Map("/session",sessionApp=> {
                sessionApp.Run(async context=> {
                    await SessionSample.SessionAsync(context);
                });
            });


            app.Map("/RequestSample",apps=> {
                apps.Run(async (context)=> {
                    string result = string.Empty;
                    switch (context.Request.Path.Value.ToLower())
                    {
                        case "/header":
                            break;
                        case "/add":

                            break;
                        case "/content":
                            result = RequestAndResponseSample.Content(context.Request);
                            break;
                        case "/encoded":
                            result = RequestAndResponseSample.ContentEncoded(context.Request);
                            break;
                        case "/form":
                            result = RequestAndResponseSample.GetForm(context.Request);
                            break;
                        case "/writecookies":
                            result = RequestAndResponseSample.WriteCookies(context.Response);
                            break;
                        case "/readcookies":
                            result = RequestAndResponseSample.ReadCookies(context.Request);
                            break;
                        case "/json":
                            result = RequestAndResponseSample.GetJson(context.Response);
                            break;
                        default:
                            result = RequestAndResponseSample.GetRequestInformation(context.Request);
                            break;
                    }
                    await context.Response.WriteAsync(HtmlHelper.DocType() + HtmlHelper.HtmlStart() + HtmlHelper.Head() + HtmlHelper.BodyStart());
                    await context.Response.WriteAsync(result);
                    await context.Response.WriteAsync(HtmlHelper.BodyEnd() + HtmlHelper.HtmlEnd());
                });
            });
        }
    }
}
