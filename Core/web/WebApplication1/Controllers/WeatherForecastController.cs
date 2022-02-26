using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
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


        /// <summary>
        /// 获取天气信息
        /// </summary>
        /// <returns>天气信息</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }



        /// <summary>
        /// 天气情况
        /// </summary>
        /// <param name="forecast">天气情况</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddWeatherForecast(WeatherForecast forecast) => Ok();


        /// <summary>
        /// 天气情况
        /// </summary>
        /// <param name="forecast">天气情况</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateWeatherForecast(WeatherForecast forecast) => Ok();


        /// <summary>
        /// 删除天气情况
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteWeatherForecast(string id)=> Ok();


        /// <summary>
        /// 修改天气情况
        /// </summary>
        /// <param name="weather">天气情况</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult> PathWeatcherForecast(WeatherForecast weather)=> Ok();

    }
}