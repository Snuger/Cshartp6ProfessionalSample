using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Db
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    [Table("SynchronousDb")]  
    public class SynchronousDb
    {  
       [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int DbType { get; set; }


        public string Ip { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; }

        public string PassWord { get; set; }

        public string InstanceName { get; set; }

        public int Enable { get; set; }

    }
}
