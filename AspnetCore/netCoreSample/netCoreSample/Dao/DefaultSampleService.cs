using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using netCoreSample.Services;

namespace netCoreSample.Dao
{
    public class DefaultSampleService : ISampleServices
    {

        private List<string> _string = new List<string> {"one","two","three"};

        public IEnumerable<string> GetSampleString() => _string;
    }
}
