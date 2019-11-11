using System;
using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    public class Product
    {
        [Key,Display(Name = "����")]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Discretion { get; set; }
    }
}