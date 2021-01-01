using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Elasticsearch.Net;
using System.Text.Json;
using ElasticSearch.Sample.Model;
using Elasticsearch.Net.Specification.CatApi;
using System.Linq;

namespace ElasticSearch.Sample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ElasticLowLevelClient _client;       
       

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ElasticLowLevelClient client, ILogger<WeatherForecastController> logger)
        {
            _client = client;
            _logger = logger;
        }

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

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

        [HttpGet("search/app")]
        public async Task<IActionResult> GetApplicationName(string searchKey)
        {
            var result = await _client.SearchAsync<StringResponse>("mcrp-*", PostData.String($@"
            {{
                ""timeout"": ""1000ms"",
                ""terminate_after"": 100000,
                ""size"": 0,
                ""aggs"": {{
                    ""termsAgg"": {{
                        ""terms"": {{
                            ""order"": {{
                                ""_count"": ""desc""
                            }},
                            ""field"": ""fields.ApplicationName.keyword"",
                            ""include"": "".*{searchKey}.*""
                        }}
                    }}
                }},
                ""docvalue_fields"": [
		               {{ ""field"": ""@timestamp"", ""format"": ""date_time""}},
		               {{""field"": ""Commit.Timestamp"", ""format"": ""date_time""}},
		               {{ ""field"": ""Object_Attributes.Created_At"", ""format"": ""date_time""}},
		               {{ ""field"": ""Object_Attributes.Finished_At"",""format"": ""date_time""}},
		               {{ ""field"": ""fields.now"",""format"": ""date_time""}}
	                ],
	                ""query"": {{
                        ""bool"": {{
                            ""must"": [],
			                ""filter"": [
                                    {{
                                        ""range"": {{
                                        ""@timestamp"": {{""format"": ""strict_date_optional_time"",
							                        ""gte"": ""{System.DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd'T'HH:mm:ss.fff")}"",
							                        ""lte"": ""{System.DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff")}"" }}
                                        }}
                                    }}
                            ],
			            ""should"": [],
                        ""must_not"": []
                    }}
                 }}
            }}"), new SearchRequestParameters() { IgnoreUnavailable = true, TotalHitsAsInteger = true });
            return Ok(result.Body);
        }


        [HttpGet("search/filed")]
        public async Task<IActionResult> GetIndexFiled(string indexName) => Ok((await _client.Indices.GetMappingAsync<StringResponse>(indexName, null)).Body);


        [HttpGet("search/index")]
        public async Task<IActionResult> GetIndex(string searchKey) => Ok((await  _client.Cat.IndicesAsync<StringResponse>(searchKey, new CatIndicesRequestParameters() { Health = Health.Green,Format="json" })).Body);



        [HttpPost("search/data")]
        public async Task<IActionResult> GetData([FromBody] QueryParame parame,int pageIndex,int pageSize) {

            var result = await _client.SearchAsync<StringResponse>("mcrp-*", PostData.String($@"
            {{
            ""timeout"": ""1000ms"",
            ""version"": true,
            ""from"":{pageIndex},
            ""size"": {pageSize},
            ""sort"": [
                {{
                ""@timestamp"": {{
                    ""order"": ""desc"",
                    ""unmapped_type"": ""boolean""
                }}
                }}
            ],
            ""_source"": {{
                ""excludes"": []
            }},
            ""stored_fields"": [
                ""*""
            ],
            ""script_fields"": {{}},
            ""docvalue_fields"": [
                {{ ""field"": ""@timestamp"", ""format"": ""date_time""}},
		        {{""field"": ""Commit.Timestamp"", ""format"": ""date_time""}},
		        {{ ""field"": ""Object_Attributes.Created_At"", ""format"": ""date_time""}},
		        {{ ""field"": ""Object_Attributes.Finished_At"",""format"": ""date_time""}},
		        {{ ""field"": ""fields.now"",""format"": ""date_time""}}
            ],
            ""query"": {{
                ""bool"": {{
                    ""must"": [],
                    ""filter"": [
                        {{
                        ""match_all"": {{}}
                        }},
                        {{
                        ""range"": {{
                            ""@timestamp"": {{
                            ""format"": ""strict_date_optional_time"",
                            ""gte"": {(parame.TimeRange.Count()<2?System.DateTime.Now.AddSeconds(-15).ToString("yyyy-MM-dd'T'HH:mm:ss.fff"):Convert.ToDateTime(parame.TimeRange?.ToArray()[0]).AddSeconds(-15).ToString("yyyy-MM-dd'T'HH:mm:ss.fff"))},
                            ""lte"": {(parame.TimeRange.Count()<2 ? System.DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss.fff") : Convert.ToDateTime(parame.TimeRange?.ToArray()[1]).ToString("yyyy-MM-dd'T'HH:mm:ss.fff"))},
                            }}
                        }}
                        }}
                    ],
                    ""should"": [],
                    ""must_not"": []
                }}
            }}
            }}"), new SearchRequestParameters() { IgnoreUnavailable = true, TotalHitsAsInteger = true });
            return Ok(result.Body);
        }
    }
}
