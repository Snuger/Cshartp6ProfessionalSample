using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.Sample.Model
{
    public class QueryParame
    {
        public string IndexName { get; set; }

        public IEnumerable<string> AppName { get; set; }

        public IEnumerable<string> Leavl { get; set; }


        public IEnumerable<string> SearchKeys { get; set; }

        public IEnumerable<string> TimeRange { get; set; }

    }
}
