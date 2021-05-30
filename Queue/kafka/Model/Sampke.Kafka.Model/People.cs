using Newtonsoft.Json;
using System;

namespace Sampke.Kafka.Model
{ 
     public class People
    {      
        [JsonRequired]  
        [JsonProperty("name")]
        public string Name { get; set; }
    
        [JsonRequired]  
        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonRequired]  
        [JsonProperty("address")]
        public string Address { get; set; }
    }
}
