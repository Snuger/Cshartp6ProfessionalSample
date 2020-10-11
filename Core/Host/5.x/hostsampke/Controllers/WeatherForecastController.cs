using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hostsampke.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace hostsampke.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IJWTService _jwtService;

        private readonly IConfiguration _iConfiguration;


        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IJWTService jWTService, IConfiguration configuration)
        {
            _logger = logger;
            _jwtService = jWTService;
            _iConfiguration = configuration;
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

        public IActionResult Login(string name, string passwd)
        {
            ///这里应该是需要去连接数据库做数据校验，为了方便所有用户名和密码写死了
            if ("admin".Equals(name) && "123456".Equals(passwd))//应该数据库
            {
                string token = _jwtService.GetToken(name);
                return new JsonResult(new
                {
                    result = true,
                    token
                });
            }
            else
            {
                return new JsonResult(new
                {
                    result = false,
                    token = string.Empty
                });
            }

        }

    }
}
