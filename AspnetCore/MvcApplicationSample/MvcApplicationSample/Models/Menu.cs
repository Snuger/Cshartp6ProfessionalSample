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
        [Key,Required]
        public int Id { get; set; }

        [DisplayName("书名"),Required(ErrorMessage ="请输入书名"),StringLength(50)]
        public string Text { get; set; }

        [Display(Name="Price"),DisplayFormat(DataFormatString ="{0:C}")]
        public double Price { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

        [StringLength(10)]
        public string Category { get; set; }
    }
}
