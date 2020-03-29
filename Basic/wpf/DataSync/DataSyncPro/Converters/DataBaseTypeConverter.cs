using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataSyncPro.Converters
{
    public class DataBaseTypeConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           int temp = (int)value;
            switch (temp) {
                case 1:
                    return "Oracle";
                case 2:
                    return "MySql";
                case 3:
                    return "SqlServer";
                case 4:
                    return "PostgreSQL";
                case 5:
                    return "HiBase";
                case 6:
                    return "MangoDB";                 
                default:
                    return null;
            }            
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strValue = value.ToString();
            return value;
        }
    }
}
