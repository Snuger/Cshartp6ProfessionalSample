using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostSample
{
    public class LifetimeEventsHostedService:IHostedService
    {
        private readonly ILogger logger;
        private readonly IApplicationLifetime applicationLifetime;

        public LifetimeEventsHostedService(ILogger logger, IApplicationLifetime applicationLifetime)
        {
            this.logger = logger;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            applicationLifetime.ApplicationStarted.Register(OnApplicationStarted);
            applicationLifetime.ApplicationStopping.Register(OnApplicationStopping);
            applicationLifetime.ApplicationStopped.Register(OnApplicationStoped);
            return Task.CompletedTask;
        }

        private void OnApplicationStoped()
        {
            logger.LogInformation("services has been stoped.");
        }

        private void OnApplicationStopping()
        {
            logger.LogInformation("services is stopping.");
        }

        private void OnApplicationStarted()
        {
            logger.LogInformation("services has been started.");
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
