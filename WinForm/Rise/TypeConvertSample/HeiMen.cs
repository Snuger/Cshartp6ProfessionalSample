using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeConvertSample
{
    [TypeConverter(typeof(StringToHeiMenTypeConverter))]
    public class HeiBoyin
    {
        public string Name { get; set; }

        public HeiBoyin Chield { get; set; }
    }
}
