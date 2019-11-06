using System;
using System.Collections.Generic;
using RabbitMQ.Client;


namespace Consumer.Controler
{
    public class ExchangeConfig
    {
        public string Name { get; set; }

        public string ExchangeType { get; set; }

        public bool Durable { get; set; }

        public bool AutoDelete { get; set; }
        public IDictionary<string, object> Arguments { get; set; }
    }
}