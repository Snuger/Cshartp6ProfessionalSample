using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeConvertSample
{
    public class StringToHeiMenTypeConverter:TypeConverter
    {
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            HeiBoyin h = new HeiBoyin();
            if (value is string)
            {              
                h.Name = value as string;               
            }
            return h;
        }
    }
}
