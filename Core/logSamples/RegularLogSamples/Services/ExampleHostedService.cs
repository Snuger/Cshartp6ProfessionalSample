using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Xml.Schema;
using RegularLogSamples.Model;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Configuration;

namespace RegularLogSamples
{
    public class ExampleHostedService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IHostLifetime _hostLifetime;
        private Timer _timer;
        private TimerCallback _timerCallback;
        private int executionCount = 0;
        private IConfiguration _config;

        private readonly ApplicationDbContext _dbContext;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="hostApplicationLifetime"></param>
        /// <param name="hostLifetime"></param>
        /// <param name="timer"></param>
        /// <param name="timerCallback"></param>
        /// <param name="executionCount"></param>
        /// <param name="config"></param>
        /// <param name="dbContext"></param>
        public ExampleHostedService(ILogger<ExampleHostedService> logger, IHostApplicationLifetime hostApplicationLifetime, IHostLifetime hostLifetime,IConfiguration config, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _hostLifetime = hostLifetime;           
            _config = config;
            _dbContext = dbContext;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStarted);
            _timerCallback = new TimerCallback(DoWork);
            _timer = new Timer(_timerCallback, null, TimeSpan.Zero,TimeSpan.FromSeconds(5));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _hostApplicationLifetime.ApplicationStarted.Register(OnStoped);
            _hostLifetime.StopAsync(cancellationToken);
            return Task.CompletedTask;
        }

        private void OnStarted()
        {
            _logger.LogInformation("OnStarted has been called.");
            _logger.LogInformation("check Data init status");
            if (!ExistsData().Result)
            {
                _logger.LogInformation("init data on start ");
                _dbContext.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                _dbContext.Blogs.Add(new Blog { Url = "http://blogs.msdn.com/f#" });
                _dbContext.SaveChangesAsync();
            }
           
        }

        private void OnStoped()
        {
            _logger.LogInformation("OnStopped has been called.");
        }

        private async Task<bool> ExistsData() {           
            return (await _dbContext.Blogs.CountAsync())>0;            
        }

        private void DoWork(object state)
        {
            executionCount++;
            _logger.LogInformation($"第几次{executionCount},系统时间：{(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"))}，取出环境变量:{(_config["DevAccount_FromLibrary"])}");
            
        }

        public void Dispose()
        {
            _timer.Dispose();
        }
    }
}