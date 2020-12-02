using System;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Sampke.Default.Consumer.Models;
using Confluent.SchemaRegistry.Serdes;
using Confluent.Kafka.SyncOverAsync;
using Sampke.Default.Consumer.Services;

namespace Sampke.Default.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        static IHostBuilder CreateHostBuilder(string [] arg) {
           return Host.CreateDefaultBuilder()
                .ConfigureServices((context,service)=> {
                    service.AddScoped<IConsumer<string, string>>(sep => new ConsumerBuilder<string, string>(
                        new ConsumerConfig() {
                            BootstrapServers = "172.25.219.207:9092",                       
                            GroupId = "csharp-consumer",
                            EnableAutoCommit = false,
                            StatisticsIntervalMs = 5000,
                            SessionTimeoutMs = 6000,
                            AutoOffsetReset = AutoOffsetReset.Earliest,
                            EnablePartitionEof = true
                        })
                       .SetErrorHandler((_,e)=>Console.WriteLine(e.Reason))
                       .SetStatisticsHandler((_,json)=>Console.WriteLine($">{json}"))
                      // .SetValueDeserializer(new JsonDeserializer<Person>().AsSyncOverAsync())
                    .Build());

                    service.AddHostedService<ConsumerService>();
                });
        }
}
    }
