using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    public class Product
    {
        [Key,Display(Name = "主键")]
        public int Id { get; set; }

        [Display(Name ="产品名称")]
        public string Name { get; set; }

        [Display(Name ="描述")]
        public string Discretion { get; set; }
    }
}