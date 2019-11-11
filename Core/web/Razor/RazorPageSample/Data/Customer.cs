using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    /// <summary>
    /// ??
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(150),Display(Name="����")]
        public string Name { get; set; }

        [Display(Name="����")]
        public int Age { get; set; }
        [Display(Name ="��ַ")]
        public string Address { get; set; }

    }
}