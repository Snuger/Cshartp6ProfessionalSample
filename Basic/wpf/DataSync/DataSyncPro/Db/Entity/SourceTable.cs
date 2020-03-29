using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSyncPro.Db.Entity
{
    [Table("SourceTables")]
    public  class SourceTable
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 表名描述
        /// </summary>
        public string Discription { get; set; }

        /// <summary>
        /// 表字段
        /// </summary>
        [ForeignKey("TableID")]
        public List<TableColumn> Columns { get; set; }


    }
}
