using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmeterIntegrated.Model
{
    public class HttpResponseResult
    {
        public string TimeStamp { get; set; }

        public string Elapsed { get; set; }

        public string ResponseCode { get; set; }

        public string ResponseMessage { get; set; }

        /// <summary>
        /// 线程数
        /// </summary>
        public string ThreadName { get; set; }

        public string DataType { get; set; }

        public string  Success { get; set; }

        public string FailureMessage { get; set; }

        public int Bytes { get; set; }

        public int SendBytes { get; set; }

        public string GrpThreads { get; set; }

        public string  AllThreads { get; set; }

        public string  Url { get; set; }

        public string  Latency { get; set; }

        public string  IdleTime { get; set; }

        public string Connect { get; set; }

    }
}
