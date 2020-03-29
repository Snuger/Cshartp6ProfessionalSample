using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApplicationSample.Models
{
    public class Menu
    {
        [Key,Required,DisplayName("编号")]
        public int Id { get; set; }

        [DisplayName("书名"),Required(ErrorMessage ="请输入书名"),StringLength(50)]
        public string Text { get; set; }

        [DisplayName("价格"),Display(Name="价格"),DisplayFormat(DataFormatString ="{0:C}")]
        public double Price { get; set; }

        [DataType(DataType.Date),DisplayName("发行时间")]
        public DateTime CreateDate { get; set; }

        [StringLength(10),DisplayName("分类")]
        public string Category { get; set; }
    }
}
