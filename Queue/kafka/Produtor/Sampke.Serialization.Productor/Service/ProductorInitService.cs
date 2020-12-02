using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sampke.Kafka.Model;
using Sampke.Serialization.Productor.utils;

namespace Sampke.Serialization.Productor.Service
{
    public class ProductorInitService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IProducer<long,Student> _producer;


        public ProductorInitService(ILogger<ProductorInitService> logger, IProducer<long, Student> producer)
        {
            _logger = logger;
            _producer = producer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Data init Start ....");
            long num = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                num += 1;
                Student student = new Student() { Name = ChineseNameGenerater.GetChineseName(), Age = new Random().Next(1,100), Address = "浙江省" };
                try
                {           
                    await _producer.ProduceAsync(nameof(Student), new Message<long, Student>()
                    {
                        Key = num,                       
                        Value = student
                    });                  
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex.Message);
                }
                await Task.Delay(100, stoppingToken);
            }

            _logger.LogInformation("Service stopping");
        }
    }

}