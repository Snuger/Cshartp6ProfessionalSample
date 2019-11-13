using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageSample.Data
{
    public class Product
    {
        [Key,Display(Name = "商品编号")]
        public int Id { get; set; }

        [Required(ErrorMessage ="产品名称不能为空"),Display(Name ="产品名称")]
        public string Name { get; set; }

        /// <summary>
        /// 商品毛重
        /// </summary>
        public double GrossWeight { get; set; }


        [Required(ErrorMessage ="描述不能为空"),Display(Name ="描述")]
        public string Discretion { get; set; }



    }
}