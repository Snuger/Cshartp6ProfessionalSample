using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPageSample.Data
{
    public class Laptop:Product
    {
        [Required,Display(Name ="系统")]
        public string SystemOs { get; set; }

        /// <summary>
        /// 厚度
        /// </summary>
        public double Thickness { get; set; }

        /// <summary>
        /// 硬盘容量
        /// </summary>
        [Required,Display(Name ="硬盘容量")]
        public int HardDriveCapacity { get; set; }

        /// <summary>
        /// Cpu
        /// </summary>
        [Required, Display(Name = "CPU")]
        public string Cpu { get; set; }

        [MaxLength(200)]
        public string Series { get; set; }

        /// <summary>
        /// 净重
        /// </summary>
       [Column(TypeName = "decimal(10, 4)")]
        public double MachineWeight { get; set; }

        /// <summary>
        /// 显卡
        /// </summary>
        public string GraphicsCard { get; set; }

        /// <summary>
        /// 色系
        /// </summary>
        public string ColorSystem { get; set; }

        /// <summary>
        /// 色域
        /// </summary>
        public string ColorArea { get; set; }


        /// <summary>
        /// 特性
        /// </summary>
        public string Characteristic { get; set; }

        /// <summary>
        ///服务
        /// </summary>
        public string Service { get; set; }

        /// <summary>
        /// 
        /// </summary>
       [Column(TypeName ="int")]
        public LaptopCategory Category { get; set; }

        [Column(TypeName = "int")]
        public LaptopMaterial Material { get; set; }
    }
}
