using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] queues = new string[] { "A", "B", "C", "D" };

            foreach (var item in queues)
            {
                Task.Run(() =>
                {
                    ConnectionFactory _factory = new ConnectionFactory
                    {
                        UserName = "guest",
                        Password = "guest",
                        HostName = "10.0.75.1"
                    };
                    var connection = _factory.CreateConnection();
                    var chanel = connection.CreateModel();
                    EventingBasicConsumer consumer = new EventingBasicConsumer(chanel);
                    consumer.Received += (ch, ea) =>
                    {
                        var message = System.Text.Encoding.UTF8.GetString(ea.Body);
                        System.Console.WriteLine($"{item}收到-->{message}");
                        chanel.BasicAck(ea.DeliveryTag, false);
                    };
                    chanel.BasicConsume(item, false, consumer);
                    System.Console.WriteLine($"{item}队列监管器已经正常启动");
                    Console.ReadKey();
                    chanel.Dispose();

                    connection.Close();
                });
            }
            Console.ReadLine();

        }
    }
}
