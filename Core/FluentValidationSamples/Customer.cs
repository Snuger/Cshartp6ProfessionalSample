using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FluentValidationSamples
{
    public class Customer
    {
        public int? Id { get; set; }
      
        [Required(ErrorMessage ="用户名必填")]
        public string UserName { get; set; }

        [DisplayName("年龄")]
        [Range(minimum:1,maximum:110,ErrorMessage ="请输入1到110的数字")]
        public int Age { get; set; }


        
    }
}