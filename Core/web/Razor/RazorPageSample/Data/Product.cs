using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPageSample.Data
{
    public class Product
    {
        [Key,Display(Name = "��Ʒ���")]
        public int Id { get; set; }

        [Required(ErrorMessage ="��Ʒ���Ʋ���Ϊ��"),Display(Name ="��Ʒ����")]
        public string Name { get; set; }

        /// <summary>
        /// ��Ʒë��
        /// </summary>
        public double GrossWeight { get; set; }


        [Required(ErrorMessage ="��������Ϊ��"),Display(Name ="����")]
        public string Discretion { get; set; }



    }
}