using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Sampke.Kafka.Model;
using Sampke.Serialization.Productor.Buillder;
using Sampke.Serialization.Productor.utils;

namespace Sampke.Serialization.Productor.Service
{
    public class ProductorInitService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IProducer<Null,CollegeStudents> _producer; 

        public ProductorInitService(ILogger<ProductorInitService> logger, SampkeProducterBuilder<CollegeStudents> studentProducerBuilder )
        {
            _logger = logger;
            _producer = studentProducerBuilder.Build();          
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Data init Start ....");
            long num = 0;
            while (!stoppingToken.IsCancellationRequested)
            {
                num += 1;
                CollegeStudents student = new CollegeStudents() { Name = ChineseNameGenerater.GetChineseName(), Age = new Random().Next(1,100), Address = "浙江省",ClassName="三年级一班", Counselor="陈进"};
                try
                {                    
                    
                    await _producer.ProduceAsync(nameof(CollegeStudents).ToLower(), new Message<Null, CollegeStudents>()
                    {                                        
                        Value = student
                    });
                     _logger.LogInformation($"{{name:{student.Name},age:{student.Age},addrss:{student.Address}}}");
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