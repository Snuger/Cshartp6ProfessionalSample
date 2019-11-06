using System;
using RabbitMQ.Client;

namespace DirectExchangeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string exchangeName = "TestChange";
            string queueName = "hello";
            string routeKey = "helloRouteKey";

            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "10.0.75.1"

            };
            var connect = factory.CreateConnection();
            var channel = connect.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);
            channel.QueueDeclare(queueName, false, false, false, null);
            channel.QueueBind(queueName, exchangeName, routeKey, null);

            Console.WriteLine($"\nRabbitMQ连接成功,Exchange：{exchangeName}，Queue：{queueName}，Route：{routeKey}，\n\n请输入消息，输入exit退出！");

            string input;
            do
            {
                input = Console.ReadLine();

                var sendBytes = System.Text.Encoding.UTF8.GetBytes(input);
                //发布消息
                channel.BasicPublish(exchangeName, routeKey, null, sendBytes);

            } while (input.Trim().ToLower() != "exit");
            channel.Close();
            connect.Close();

        }
    }
}
