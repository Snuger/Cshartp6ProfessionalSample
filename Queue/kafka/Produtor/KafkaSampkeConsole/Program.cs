using System;
using Confluent.Kafka;
using KafkaSampkeConsole.Models;
using KafkaSampkeConsole.utils;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

namespace KafkaSampkeConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Console.WriteLine("Hello World!");
            ProducerConfig config = new ProducerConfig() { BootstrapServers = "172.25.219.41:32771" };
            using(var productor=new ProducerBuilder<string, string>(config).Build())
            {              
                var cancelled = false;
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };
                while (!cancelled) {

                    var people = JsonSerializer.Serialize(new People() { Name = ChineseNameGenerater.GetChineseName(), Address = "浙江省杭州市", Age = 22 });
                    Console.WriteLine($"> {people}");
                    productor.ProduceAsync(topic: nameof(People), new Message<string, string>() { Key=System.Guid.NewGuid().ToString(),Value=people });
                    Task.Delay(100);
                }
            }       
        }
    }
}
