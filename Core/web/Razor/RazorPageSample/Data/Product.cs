using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageSample.Data
{
    [Serializable]
    public class Product
    {
        [Key,Display(Name = "��Ʒ���")]
        public int Id { get; set; }

        [Required(ErrorMessage ="���Ʋ���Ϊ��"),Display(Name ="����")]
        public string Name { get; set; }

        /// <summary>
        /// ��Ʒë��
        /// </summary>
        public double GrossWeight { get; set; }


        [Required(ErrorMessage ="��������Ϊ��"),StringLength(1500,MinimumLength =5),Display(Name ="����")]
        public string Discretion { get; set; }



    }
}