namespace WebApplication1
{
    public class WeatherForecast
    {
     
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 华氏温度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 天气
        /// </summary>
        public string? Summary { get; set; }
    }
}