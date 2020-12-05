using System;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection; 
using Confluent.SchemaRegistry.Serdes;
using Confluent.Kafka.SyncOverAsync;  
using Confluent.SchemaRegistry;
using Sampke.Kafka.Model;

namespace Sampke.Serialization.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string [] args) {
           return Host.CreateDefaultBuilder()
                .ConfigureServices((context,service)=> {                  

                    service.AddScoped<IConsumer<string, Student>>(sep => new ConsumerBuilder<string, Student>(
                        new ConsumerConfig() {
                            BootstrapServers = "172.25.211.90:9092",                       
                            GroupId = $"{nameof(Student).ToLower()}-consumer-clinet",
                            EnableAutoCommit = false,
                            StatisticsIntervalMs = 5000,
                            SessionTimeoutMs = 6000,
                            AutoOffsetReset = AutoOffsetReset.Earliest,
                            EnablePartitionEof = true
                        })
                       .SetErrorHandler((_,e)=>Console.WriteLine(e.Reason))
                       .SetStatisticsHandler((_,json)=>Console.WriteLine($">{json}"))
                       .SetValueDeserializer(new JsonDeserializer<Student>().AsSyncOverAsync())
                    .Build());

                    service.AddHostedService<ConsumerService>();
                });
        }
}
    }
