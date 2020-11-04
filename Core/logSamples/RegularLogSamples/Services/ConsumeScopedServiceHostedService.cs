using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegularLogSamples.Services
{
    public class ConsumeScopedServiceHostedService : BackgroundService
    {

        private ILogger<ConsumeScopedServiceHostedService> _logger;

        public IServiceProvider Services { get; }

        public ConsumeScopedServiceHostedService(ILogger<ConsumeScopedServiceHostedService> logger, IServiceProvider services)
        {
            _logger = logger;
            Services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service running.");
            await DoWork(stoppingToken);
        }


        private async Task DoWork(CancellationToken stoppingToken) {

            _logger.LogInformation("Consume Scoped Service Hosted Service is working.");
            using (var scope = Services.CreateScope())
            {
                var scopedProcessingService =scope.ServiceProvider.GetRequiredService<IScopedProcessingService>();
                await scopedProcessingService.DoWork(stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consume Scoped Service Hosted Service is stopping.");
            return base.StopAsync(cancellationToken);
        }
    }
}
