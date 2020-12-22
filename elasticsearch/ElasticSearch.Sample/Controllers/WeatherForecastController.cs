using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;

namespace ElasticSearch.Sample.Controllers
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

        //curl -XGET "http://172.19.30.190:9400/_msearch?rest_total_hits_as_int=true&ignore_throttled=true" -H 'Content-Type: application/json' -d'{"index":"mcrp-*","ignore_unavailable":true,"preference":1608602355239}{"timeout":"1000ms","terminate_after":100000,"size":0,"aggs":{"termsAgg":{"terms":{"order":{"_count":"desc"},"field":"fields.ApplicationName.keyword","include":".*jiao.*"}}},"_source":{"excludes":[]},"stored_fields":["*"],"script_fields":{},"docvalue_fields":[{"field":"@timestamp","format":"date_time"},{"field":"Commit.Timestamp","format":"date_time"},{"field":"Object_Attributes.Created_At","format":"date_time"},{"field":"Object_Attributes.Finished_At","format":"date_time"},{"field":"fields.now","format":"date_time"}],"query":{"bool":{"must":[],"filter":[{"range":{"@timestamp":{"format":"strict_date_optional_time","gte":"2019-12-22T02:12:46.622Z","lte":"2020-12-22T02:12:46.623Z"}}}],"should":[],"must_not":[]}}}'




        [HttpGet("search")]
        public async Task<IActionResult> GetActionMethodName(string searchKey)
        {

            var node = new Uri("http://172.19.30.190:9400/mcrp-*/_search");
            var config = new ConnectionConfiguration(node);
            var client = new ElasticLowLevelClient(config);

            List<string> queryJson = new List<string>();
            queryJson.Add("{\"timeout\":"\1000ms"\,"\terminate_after"\:100000,"\size"\:0,"\aggs"\:{"\termsAgg"\:{"\terms"\:{"\order"\:{"\_count"\:"\desc"\},"\field"\:"\fields.ApplicationName.keyword"\,"\include"\:"\.* jiao.* "\}}},"\_source"\:{"\excludes"\:[]},"\stored_fields"\:["\ * "\],"\script_fields"\:{},"\docvalue_fields"\:[{"\field"\:"\@timestamp"\,"\format"\:"\date_time"\},{"\field"\:"\Commit.Timestamp"\,"\format"\:"\date_time"\},{"\field"\:"\Object_Attributes.Created_At"\,"\format"\:"\date_time"\},{"\field"\:"\Object_Attributes.Finished_At"\,"\format"\:"\date_time"\},{"\field"\:"\fields.now"\,"\format"\:"\date_time"\}],"\query"\:{"\bool"\:{"\must"\:[],"\filter"\:[{"\range"\:{"\@timestamp"\:{"\format"\:"\strict_date_optional_time"\,"\gte"\:"\2019 - 12 - 22T02: 12:46.622Z"\,"\lte"\:"\2020 - 12 - 22T02: 12:46.623Z"\}}}],"\should"\:[],"\must_not"\:[]}}}");


            client.SearchAsync(PostData.MultiJson(new List<string>()
            {


            }), new SearchRequestParameters()
            {
                IgnoreThrottled = true,
                TotalHitsAsInteger = true

            });

            return Ok();
        }
    }
}
