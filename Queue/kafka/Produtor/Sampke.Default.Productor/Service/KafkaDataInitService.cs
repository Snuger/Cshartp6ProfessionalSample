using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Sampke.Default.Productor.Models;
using Sampke.Default.Productor.utils;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;



namespace Sampke.Default.Productor.Service
{
    public class KafkaDataInitService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IProducer<long,string> _producer;


        public KafkaDataInitService(ILogger<KafkaDataInitService> logger, IProducer<long, string> producer)
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
                Person person = new Person() { ID=num,Name = ChineseNameGenerater.GetChineseName(), Age = new Random().Next(1,100), Address = "浙江省" };
                _logger.LogInformation($"初始化第{num}条数据->{JsonConvert.SerializeObject(person)}->{DateTime.Now.ToLongTimeString()}");
                try
                {
                    await _producer.ProduceAsync(nameof(Person), new Message<long, string>()
                    {
                        Key = num,                       
                        Value = JsonConvert.SerializeObject(person)
                    });
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(null, ex);
                }
                //await Task.Delay(10, stoppingToken);
            }

            _logger.LogInformation("Service stopping");
        }
    }

}