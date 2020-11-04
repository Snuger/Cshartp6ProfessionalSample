using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RegularLogSamples.Services
{
   internal  interface IScopedProcessingService
    {
        Task DoWork(CancellationToken stoppingToken);
    }
}
