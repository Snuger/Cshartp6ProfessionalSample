using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Sampke.Kafka.Model;

using Microsoft.Extensions.Hosting;

namespace Sampke.Serialization.Consumer
{
    class ConsumerService : BackgroundService
    {
        private readonly IConsumer<string,Student> _consumer;

        public ConsumerService(IConsumer<string, Student> consumer)
        {
            _consumer = consumer;
        }

        const int commitPeriod = 5;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _consumer.Subscribe(nameof(Student));

             return  Task.Run(()=> {
                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            var cr = _consumer.Consume(stoppingToken);
                             if (cr.IsPartitionEOF)
                             {
                                 Console.WriteLine($"Reached end of topic {cr.Topic}, partition {cr.Partition}, offset {cr.Offset}.");
                                 continue;
                             }                                                    
                             Console.WriteLine($"->{cr.Message?.Value}");  
                             if (cr.Offset % commitPeriod == 0)
                             {                                
                                 _consumer.Commit(cr);                              
                             }
                            
                         }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"Consume error: {ex.Error.Reason}");
                        }                        
                    }
                }
                catch (OperationCanceledException)
                {
                    _consumer.Close();
                }
            });    
        }
    }
}
