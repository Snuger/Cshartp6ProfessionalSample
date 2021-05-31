using System;
using Confluent.Kafka;
using Sampke.Console.Productor.Models;
using Sampke.Console.Productor.utils;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

namespace Sampke.Console.Productor
{
    class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // Console.WriteLine("Hello World!");
            ProducerConfig config = new ProducerConfig() { BootstrapServers = "172.20.90.54:9092" };

            using (var productor = new ProducerBuilder<Null, string>(config).Build())
            {
                Random random = new Random();
                var cancelled = false;
                System.Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cancelled = true;
                };
                while (!cancelled)
                {
                    var student = new CollegeStudents() { ClassName = "三年级一班", Counselor = ChineseNameGenerater.GetChineseName(), Name = ChineseNameGenerater.GetChineseName(), Address = "浙江省杭州市", Age = random.Next(20, 100) };
                    System.Console.WriteLine($"---->{student.ToString()}");
                    productor.Produce(topic: nameof(CollegeStudents), new Message<Null, string>() { Value = student.ToString() });
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
