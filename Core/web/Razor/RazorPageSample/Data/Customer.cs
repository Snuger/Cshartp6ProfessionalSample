using System.ComponentModel.DataAnnotations;

namespace RazorPageSample.Data
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Address { get; set; }

    }
}