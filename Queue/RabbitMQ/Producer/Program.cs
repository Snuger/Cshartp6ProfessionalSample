using System;
using RabbitMQ.Client;
using System.Text;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "guest",
                Password = "guest",
                HostName = "10.0.75.1"
            };
            var connect = factory.CreateConnection();
            var channel = connect.CreateModel();
            channel.QueueDeclare("hello", false, false, false, null);
            System.Console.WriteLine("队列管理器已经成功连接上，请输入消息，或者输入exit退出");
            string input = string.Empty;
            do
            {
                input = Console.ReadLine();
                var sendBytes = Encoding.UTF8.GetBytes(input);
                channel.BasicPublish("", "hello", null, sendBytes);

            } while (input.Trim().ToLower() != "exit");
            channel.Close();
            connect.Close();
        }
    }
}
