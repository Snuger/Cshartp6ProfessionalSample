using System;
using System.Text;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampke.Kafka.Model;
using Sampke.Serialization.Productor.Buillder;
using Sampke.Serialization.Productor.Service;

namespace Sampke.Serialization.Productor
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] arg) =>
        Host.CreateDefaultBuilder()
        .ConfigureServices((context, service) =>
        {
            service.AddScoped<ISchemaRegistryClient>(x => new CachedSchemaRegistryClient(new SchemaRegistryConfig() { Url = "http://192.168.164.224:8081" }));
            service.AddScoped(x => new ProducerConfig() { BootstrapServers = "192.168.164.224:9092" });
            service.AddScoped<SampkeProducterBuilder<Student>>();
            service.AddScoped<SampkeProducterBuilder<CollegeStudents>>();
            service.AddHostedService<ProductorInitService>();
        });

    }
}
