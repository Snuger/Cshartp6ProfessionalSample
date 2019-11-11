using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    /// <summary>
    /// ??
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(150),Display(Name="ĞÕÃû")]
        public string Name { get; set; }

        [Display(Name="ÄêÁä")]
        public int Age { get; set; }
        [Display(Name ="µØÖ·")]
        public string Address { get; set; }

    }
}