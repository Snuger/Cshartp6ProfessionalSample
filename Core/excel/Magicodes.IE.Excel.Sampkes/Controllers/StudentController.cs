using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Excel;
using Magicodes.IE.Excel.Sampkes.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Magicodes.IE.Excel.Sampkes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> getStudentExcel()
        {
            var _path = Path.Combine("excel", "test.xlsx");


            IExporter exporter = new ExcelExporter();
            var result = await exporter.Export(Path.Combine("excel", "test.xlsx"), new List<StudentDto>()
                {
                    new StudentDto
                    {
                        Name = "MR.A",
                        Age = 18,
                        Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new StudentDto
                    {
                        Name = "MR.B",
                        Age = 19,
                        Remarks = "我叫MR.B,今年19岁",
                        Birthday=DateTime.Now
                    },
                    new StudentDto
                    {
                        Name = "MR.C",
                        Age = 20,
                        Remarks = "我叫MR.C,今年20岁",
                        Birthday=DateTime.Now
                    }
                });
            return File("test.xlsx", "application/ms-excel", result.FileName);
        }

    }
}