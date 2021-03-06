﻿using System;
using System.Text;
using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sampke.Kafka.Model;
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
            service.AddScoped<IProducer<long, Student>>(x => {
            return new ProducerBuilder<long, Student>(new ProducerConfig { BootstrapServers = "172.25.211.90:9092" })
            .SetValueSerializer(new JsonSerializer<Student>(
                 new CachedSchemaRegistryClient(new SchemaRegistryConfig() { Url = "http://json-schema.org/draft-07/schema#" }),
                 new JsonSerializerConfig() { BufferBytes = 100 }
                ))           
             .Build(); });
            service.AddHostedService<ProductorInitService>();
        });

    }
}
