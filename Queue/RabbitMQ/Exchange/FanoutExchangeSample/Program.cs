using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace FanoutExchangeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string exchangeName = "Fanout";
            string routeKey = "fanoutkey";
            string[] queueNames = new string[] { "A", "B", "C", "D" };

            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "10.0.75.1"
            };
            var connect = factory.CreateConnection();
            var chanel = connect.CreateModel();
            chanel.ExchangeDeclare(exchangeName, ExchangeType.Fanout, false, false, null);

            foreach (var item in queueNames)
            {
                chanel.QueueDeclare(item, false, false);
                chanel.QueueBind(item, exchangeName, routeKey);
            }
            Console.WriteLine($"\nRabbitMQ连接成功，\n\n请输入消息，输入exit退出！");

            string input = string.Empty;
            do
            {
                input = Console.ReadLine();
                var sendMsg = System.Text.Encoding.UTF8.GetBytes(Console.ReadLine());
                chanel.BasicPublish(exchangeName, routeKey, null, sendMsg);

            } while (input != "exit");
            chanel.Close();
            connect.Close();
            connect.Dispose();
        }
    }
}
