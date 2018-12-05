using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Models
{
    public class Zoning
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string PostCode { get; set; }

        public int ParentCode { get; set; }
    }
}
