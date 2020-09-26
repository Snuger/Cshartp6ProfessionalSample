using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiRequired.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiRequired.Controllers
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

        [HttpPost]
        public IActionResult AddStudent([FromBody] Students student)
        {
            return Ok(student);
        }

        [HttpGet("mock")]
        public IActionResult GetStudent()
        {
            return Ok(new Students()
            {
                Name = "张三",
                Address = "浙江省杭州市西湖区",
                Age = 18,
                Email = "explemsnii@outlook.com",
                Friend = new Friend()
                {
                    Name = "李四",
                    Age = 199
                }
            });
        }
    }
}
