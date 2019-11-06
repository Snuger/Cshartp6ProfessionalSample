using System;
using System.Collections.Generic;

namespace Consumer.Controler
{
    public class QueueConfig
    {
        public string QueueName { get; set; }

        public bool Exclusive { get; set; }

        public bool Durable { get; set; }

        public bool AutoDelete { get; set; }

        public IDictionary<string, object> Arguments { get; set; }

        public List<ExchangeConfig> Exchanges { get; set; }

    }
}