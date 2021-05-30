using Newtonsoft.Json;

namespace Sampke.Kafka.Model
{
    public class CollegeStudents:People
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