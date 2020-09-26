using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Extension;
using Magicodes.ExporterAndImporter.Excel;
using Megicodes.execl.Sample.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Megicodes.execl.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }


        [HttpGet("excel")]
        public async Task<IActionResult> ExportHelper()
        {
            IExporter exporter = new ExcelExporter();
            //var filePath = "h.xlsx";
            var result = await exporter.ExportHeaderAsByteArray(Summaries);
            return new FileContentResult(result, "application/octet-stream");
        }


        [HttpGet("exporthead.xlsx")]
        public async Task<IActionResult> ExportHeader()
        {
            IExporter exporter = new ExcelExporter();
            var result = await exporter.ExportHeaderAsByteArray<Student>(new Student());
            return new FileContentResult(result, "application/octet-stream");
        }

        [HttpGet("student.xlsx")]
        public async Task<IActionResult> exportExcel()
        {
            IExporter exporter = new ExcelExporter();
            var result = await exporter.ExportAsByteArray(new List<Student>()
                {
                    new Student
                    {
                        Name = "MR.A",
                        Age = 18,
                         Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new Student
                    {
                        Name = "MR.B",
                        Age = 19,
                         Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new Student
                    {
                        Name = "MR.C",
                        Age = 20,
                         Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                     new Student
                    {
                        Name = "MR.D",
                        Age = 18,
                        Remarks = "我叫MR.A,今年18岁",
                        Birthday=DateTime.Now
                    },
                    new Student
                    {
                        Name = "MR.E",
                        Age = 19,
                        Remarks = "我叫MR.B,今年19岁",
                        Birthday=DateTime.Now
                    },
                    new Student
                    {
                        Name = "MR.F",
                        Age = 20,
                        Remarks = "我叫MR.C,今年20岁",
                        Birthday=DateTime.Now
                    }
                });
            return new FileContentResult(result, "application/octet-stream");
        }
    }
}
