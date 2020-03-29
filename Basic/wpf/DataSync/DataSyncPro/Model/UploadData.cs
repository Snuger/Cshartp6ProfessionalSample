using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Model
{
    public class UploadDataOption: ViewModelBase
    {    
        public int Id { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }


        /// <summary>
        /// 表描述
        /// </summary>
        public string TableDiscription { get; set; }


        /// <summary>
        /// 操作范围(yyyy-mm-dd)
        /// </summary>
        public string OperatingRange { get; set; }


        /// <summary>
        /// 是否
        /// </summary>
        public bool IsComplated { get; set; }  
    }
}
