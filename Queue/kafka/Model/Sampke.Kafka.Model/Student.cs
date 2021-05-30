using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sampke.Kafka.Model
{
    
    public class Student:People
    {        
        /// <summary>
        /// 班级名称
        /// </summary>      
        [JsonRequired]  
        [JsonProperty("className")]
        public string ClassName { get; set; }

        /// <summary>
        /// 辅导员
        /// </summary>       
        [JsonRequired]  
        [JsonProperty("counselor")]
        public string Counselor { get; set; }
    }
}
