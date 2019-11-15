using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageSample.Data
{
    [Serializable]
    public class Product
    {
        [Key,Display(Name = "商品编号")]
        public int Id { get; set; }

        [Required(ErrorMessage ="名称不能为空"),Display(Name ="名称")]
        public string Name { get; set; }

        /// <summary>
        /// 商品毛重
        /// </summary>
        public double GrossWeight { get; set; }


        [Required(ErrorMessage ="描述不能为空"),StringLength(1500,MinimumLength =5),Display(Name ="描述")]
        public string Discretion { get; set; }



    }
}