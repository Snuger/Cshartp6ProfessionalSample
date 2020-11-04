using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegularLogSamples.Services
{
    internal class ScopedProcessingService : IScopedProcessingService
    {

        private readonly ILogger<ScopedProcessingService> _logger;
        private int executionCount = 0;

        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
        {
            _logger = logger;
        }

        public async Task DoWork(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                executionCount++;
                _logger.LogInformation("Scoped Processing Service is working. Count: {Count}", executionCount);
                await Task.Delay(10000, stoppingToken);
            }
        }
    }
}
