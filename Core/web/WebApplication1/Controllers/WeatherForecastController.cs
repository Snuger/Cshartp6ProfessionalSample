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
        /// ��ȡ������Ϣ
        /// </summary>
        /// <returns>������Ϣ</returns>
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
        /// �������
        /// </summary>
        /// <param name="forecast">�������</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> AddWeatherForecast(WeatherForecast forecast) => Ok();


        /// <summary>
        /// �������
        /// </summary>
        /// <param name="forecast">�������</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult> UpdateWeatherForecast(WeatherForecast forecast) => Ok();


        /// <summary>
        /// ɾ���������
        /// </summary>
        /// <param name="id">����</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteWeatherForecast(string id)=> Ok();


        /// <summary>
        /// �޸��������
        /// </summary>
        /// <param name="weather">�������</param>
        /// <returns></returns>
        [HttpPatch]
        public async Task<ActionResult> PathWeatcherForecast(WeatherForecast weather)=> Ok();

    }
}