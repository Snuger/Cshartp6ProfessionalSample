using System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer.Controler
{
    public class QueueListener
    {

        public IConnectionFactory ConnectionFactory { get; set; }

        public QueueConfig QueueConfig { get; set; }

        public AsyncEventHandler<BasicDeliverEventArgs> ReceivedEvent;

        protected AsyncEventingBasicConsumer Consumer { get; set; }

        protected IModel Channel { get; }

        protected IConnection Connection { get; }



        public QueueListener(IConnectionFactory connectionFactory, QueueConfig config)
        {
            ConnectionFactory = connectionFactory;
            Connection = connectionFactory.CreateConnection();
            Channel = Connection.CreateModel();
            Consumer = new AsyncEventingBasicConsumer(this.Channel);
            Consumer.Received += ReceivedEvent;
        }


        public void Start()
        {
            using (Connection)
            {
                using (Channel)
                {
                    if (this.Channel != null && this.Channel.IsOpen)
                    {
                        this.Channel.BasicConsume(this.QueueConfig.QueueName, false, Consumer);
                    }
                }
            }
        }

        public void stop()
        {

        }
    }
}