using System;
using RabbitMQ.Client;

namespace TopicExchangeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string queueName = "GG";
            string exchangeName = "TopicExchange";
            string routeKey = "topicSample.*";

            ConnectionFactory _factory = new ConnectionFactory()
            {
                HostName = "10.0.75.1",
                UserName = "guest",
                Password = "guest"
            };

            var connect = _factory.CreateConnection();
            var channel = connect.CreateModel();
            channel.ExchangeDeclare(exchangeName, ExchangeType.Topic, false, false, null);
            channel.QueueDeclare(queueName, false, false, false, null);

            channel.QueueBind(queueName, exchangeName, routeKey, null);
            System.Console.WriteLine("RabbitMQ服务器已经成功连接，请输入消息");

            string input = string.Empty;
            do
            {
                input = Console.ReadLine();
                var sendByte = System.Text.Encoding.UTF8.GetBytes(input);
                channel.BasicPublish(exchangeName, "topicSample.one", false, null, sendByte);


            } while (input.ToLower() != "exit");

        }
    }
}
