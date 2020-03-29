using System;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Collections.Generic;
using Consumer.Controler;

namespace Consumer
{
    public class ConsumerManager
    {
        protected List<QueueListener> Listeners { get; set; }

        public void Add(QueueListener listener)
        {
            lock (listener)
            {
                this.Listeners.Add(listener);
            }
        }

        public void Start()
        {
            Listeners.ForEach(item =>
            {
                item.Start();
            });
        }

        public void Stop() { }
    }

}