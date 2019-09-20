#define HostManaer
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebHostSample
{
    class Program
    {
#if TemplateCode
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
           
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args)=>        
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

#elif LogFromMain
        static void Main(string[] args) {
            var host=CreateWebHostBuilder(args).Build();
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("seed the database");
            host.Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logger =>
            {
                logger.ClearProviders();
                logger.AddConsole();
            });
#elif ExpandDefault
        static void Main(string[] args) {
            var host = new WebHostBuilder()
                .UseKestrel((context, opt) => {
                    opt.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(2);

                })
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, app) =>
                {
                    var env = context.HostingEnvironment;
                    app.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                    app.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    app.AddEnvironmentVariables();
                    app.AddCommandLine(args);
                })
                .ConfigureLogging((context, logger) =>
                {
                    logger.AddConfiguration(context.Configuration.GetSection("logging"));//读取日志配置文件
                    logger.AddFilter("System", LogLevel.Debug); //添加日志筛选
                    logger.AddFilter((provider, category, loglevel) => { //自定义筛选方法
                        if (provider == "Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider" &&
                            category == "TodoApiSample.Controllers.TodoController")
                        {
                            return false;
                        }
                        else {
                            return true;
                        }
                    });

                    logger.SetMinimumLevel(LogLevel.Warning);//设置最小的日志输出级别                                    
                    logger.AddConsole(opt => { //将日志打印至控制台
                        opt.DisableColors = false; //禁用控制台颜色
                        opt.IncludeScopes = true;
                    });
                    logger.AddDebug(); //添加调试日志
                    logger.AddEventSourceLogger(); //添加事件源记录器
                })
                .UseUrls("http://*:8000/")
                .UseStartup<Startup>()
                //.UseSetting(WebHostDefaults.ApplicationKey, "应用程序显示名称") //应用程序键（名称）
                 //.UseSetting(WebHostDefaults.HostingStartupExcludeAssembliesKey, "assembly1;assembly1;")
                //.UseSetting(WebHostDefaults.HostingStartupAssembliesKey, "assembly1;assembly1;") //承载器加载指定的程序集
                // .UseSetting(WebHostDefaults.PreventHostingStartupKey, "true") //阻止承载器                
                .CaptureStartupErrors(true) //捕获启动异常
                .UseContentRoot(Directory.GetCurrentDirectory()) //此设置确定 ASP.NET Core 开始搜索内容文件，如 MVC 视图等。
                .UseShutdownTimeout(TimeSpan.FromMinutes(10))
                //.UseWebRoot("public")//设置静态资源的相对路径 默认路径为应用程序的根目录wwwroot
                .Build();
            // host.Run(); //启动Web应用并阻止调用线程，直到关闭机器

            //  host.Start(); //非阻止方式运行主机；
            using (host)
            {
                host.Start();
                Console.WriteLine("use Ctrl-C to shutdown the host....");
                host.WaitForShutdown();

            }
            

        }
#elif HostManaer
        static void Main(string[] args)
        {          
            string [] urls = new string[] {
                 "http://*:8088",
                "http://localhost:8089"
            };
           var host= new WebHostBuilder()
                .UseKestrel()                
                .UseStartup<Startup>()
                .Start(urls);
            using (host)
            {
                Console.ReadLine();
            }
        }
#endif

    }


}
