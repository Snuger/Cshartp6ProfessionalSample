using System;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;

namespace Megicodes.execl.Sample.Model
{

    [ExcelExporter(Name = "学生信息", TableStyle = "Light10", AutoFitAllColumn = true)]
    public class Student
    {
        /// <summary>
        ///     姓名
        /// </summary>
        [ExporterHeader(DisplayName = "姓名")]
        public string Name { get; set; }
        /// <summary>
        ///     年龄
        /// </summary>
        [ExporterHeader(DisplayName = "年龄")]
        public int Age { get; set; }

        [ExporterHeader(DisplayName = "备注")]
        public string Remarks { get; set; }


        [ExporterHeader(DisplayName = "出生日期", Format = "yyyy-mm-DD")]
        public DateTime? Birthday { get; set; }
    }
}