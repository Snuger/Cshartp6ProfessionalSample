using System;
using System.Collections.Generic;
using System.Text;

namespace StringSample
{
    public partial class Persion:IFormattable
    {
        public Persion(string firsName, string lastName)
        {
            FirsName = firsName;
            LastName = lastName;
        }

        public string FirsName { get; set; }

        public string LastName { get; set; }

        public virtual string ToString(string format) => ToString(format, null);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format) {
                case "F":
                    return $"你好，我姓:{FirsName}";                   
                case "L":
                    return $"我的名字叫:{LastName}";                    
                case "All":
                    return $"我的全名:{LastName}.{FirsName}";                   
                default:
                    return base.ToString();            
            }            
        }

        public override string ToString() => $"{LastName}.{FirsName}";
    }
}
