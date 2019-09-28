using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostSample
{
    public class QueuedHostedService:IHostedService
    {
        private readonly ILogger logger;

        private CancellationTokenSource _shutdown = new CancellationTokenSource();

        private Task BackgroundTask;

        public QueuedHostedService(ILogger logger, IBackgroundTaskQueue taskQueue)
        {
            this.logger = logger;
            TaskQueue = taskQueue;
        }

        public IBackgroundTaskQueue TaskQueue { get; }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Queued Hosted Service is starting.");
            BackgroundTask = Task.Run(BackgroundProcess);
            return Task.CompletedTask;
        }

        private async Task BackgroundProcess()
        {
            while (!_shutdown.IsCancellationRequested)
            {
                var workitem = await TaskQueue.DequeueAsync(_shutdown.Token);
                try
                {
                    await workitem(_shutdown.Token);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, $"Error occurred executing {nameof(workitem)}.");
                }


            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Queued Hosted Service is stopping.");
            _shutdown.Cancel();

            return Task.WhenAny(BackgroundTask,
                Task.Delay(Timeout.Infinite, cancellationToken));
        }
    }
}
