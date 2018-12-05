using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreSample.Services
{
    public interface ISampleServices
    {
        IEnumerable<string> GetSampleString();

    }
}
