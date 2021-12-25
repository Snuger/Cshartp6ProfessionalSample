using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StaticFileMeddlewareApi.Controllers
{

    [Produces("application/json")]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// 随机生成温度信息
        /// </summary>
        /// <param name="name">名称</param>
        /// <returns>返回体温表集合</returns>     
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<WeatherForecast>))]
        [ProducesResponseType(StatusCodes.Status404NotFound,Type=typeof(string))]
        [HttpGet]
        public IEnumerable<WeatherForecast> Get(string name)
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

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="weather">气温对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] WeatherForecast weather) {
            return Ok();
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="weather">气温对象</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] WeatherForecast weather) {
            return Ok();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(string id) {
            return Ok();
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="weather">气温对象</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody] WeatherForecast weather) {
            return Ok();
        }
    }
}
