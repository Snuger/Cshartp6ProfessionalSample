using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    public class Product
    {
        [Key,Display(Name = "����")]
        public int Id { get; set; }

        [Display(Name ="��Ʒ����")]
        public string Name { get; set; }

        [Display(Name ="����")]
        public string Discretion { get; set; }
    }
}