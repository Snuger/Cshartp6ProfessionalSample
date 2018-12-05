using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Models
{
    public class Students
    {
        /// <summary>
        ///编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年纪
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 固定电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string TelPhone { get; set; }
    }
}
