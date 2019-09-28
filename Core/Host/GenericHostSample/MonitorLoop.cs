using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GenericHostSample
{
    public class MonitorLoop
    {
        private readonly IBackgroundTaskQueue _taskQueue;

        private readonly ILogger _logger;

        private readonly CancellationToken _cancellationToken;

        public MonitorLoop(IBackgroundTaskQueue taskQueue, ILogger<MonitorLoop> logger,IApplicationLifetime applicationLifetime)
        {
            _taskQueue = taskQueue;
            _logger = logger;
            _cancellationToken = applicationLifetime.ApplicationStopping;
        }

        public void StartMonitorLoop()
        {
            Task.Run(()=> Monitor());

        }

        public void Monitor() {
            Console.WriteLine();
            Console.WriteLine("Tap W to add a work item to the background queue ...");
            Console.WriteLine();

            while (!_cancellationToken.IsCancellationRequested)
            {
                var keyStroke = Console.ReadKey();

                if (keyStroke.Key == ConsoleKey.W)
                {                   
                    _taskQueue.QueueBackgroundWorkItem(async token =>
                    {
                        var guid = Guid.NewGuid().ToString();

                        for (int delayLoop = 0; delayLoop < 3; delayLoop++)
                        {
                            _logger.LogInformation(
                                $"Queued Background Task {guid} is running. {delayLoop}/3");
                            await Task.Delay(TimeSpan.FromSeconds(5), token);
                        }

                        _logger.LogInformation(
                            $"Queued Background Task {guid} is complete. 3/3");
                    });
                }
            }
        }



    }
}
