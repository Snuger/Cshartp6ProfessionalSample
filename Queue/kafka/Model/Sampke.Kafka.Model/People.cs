using Newtonsoft.Json;
using System;

namespace Sampke.Kafka.Model
{
     public class People
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public int Age { get; set; }

        [JsonRequired]
        public string Address { get; set; }
    }
}
