using System;
using System.Text;
using Confluent.Kafka;
using Sampke.Default.Productor.Models;
using Sampke.Default.Productor.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Sampke.Default.Productor
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
            service.AddScoped<IProducer<long, string>>(x => { return new ProducerBuilder<long, string>(new ProducerConfig { BootstrapServers = "172.25.219.207:9092" }).Build(); });
            //service.AddScoped<IProducer<long,Person>>(x => (new ProducerBuilder<long, Person>(new ProducerConfig() { BootstrapServers = "localhost:9092" }).Build()));
            service.AddHostedService<KafkaDataInitService>();
        });

    }
}
