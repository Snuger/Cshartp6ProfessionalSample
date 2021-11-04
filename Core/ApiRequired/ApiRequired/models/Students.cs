using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace ApiRequired.models
{
    public class Students
    {

        [Required(ErrorMessage = "姓名不能为空")]
        public string Name { get; set; }

        [Required(ErrorMessage = "地址不能为空")]
        public string Address { get; set; }

        [Range(0, 100, ErrorMessage = "不到 18 岁不能注册，并请填写合适范围的值")]
        public int Age { get; set; }

        [EmailAddress(ErrorMessage = "邮箱格式错误")]
        public string Email { get; set; }

        [JsonPropertyName("time_begin")]
        public DateTime TimeBigin { get; set; }

    }

    public class Friend
    {

        [Required(ErrorMessage = "朋友的姓名不能为空")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "老铁，年龄不能乱来")]
        public int Age { get; set; }
    }
}