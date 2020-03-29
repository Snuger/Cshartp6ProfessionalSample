using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Db.Entity
{
    [Table("SourceTableColumn")]
    public class TableColumn
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int  DataType { get; set; }
      
        public int TableID { get; set; }


        /// <summary>
        /// 描述
        /// </summary>
        public string Discription { get; set; }


    }
}
