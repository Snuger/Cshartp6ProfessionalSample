using System;
using System.Collections.Generic;
using System.Text;

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Models
{
    /// <summary>
    /// BaseModelInfo
    /// </summary>
    public class BaseModelInfo
    { 
        /// <summary>
       /// 参数类型
       /// </summary>
        public object 参数类型 { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string 描述 { get; set; }
        /// <summary>
        /// 可空类型
        /// </summary>
        public bool 可空类型 { get; set; }
    }
}
