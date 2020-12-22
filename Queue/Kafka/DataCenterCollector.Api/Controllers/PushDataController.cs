using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using DataCenterCollector.Api.Dtos;
using System.Text.Json;

namespace DataCenterCollector.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PushDataController : ControllerBase
    {
        private readonly IProducer<string, string> _producer;

        protected const string topicName = "mcrp-fink-test";


        public PushDataController(IProducer<string, string> producer)
        {
            _producer = producer;
        }


        [HttpPost]
        public async Task<IActionResult> PostData(string json) => Ok(await _producer.ProduceAsync(topicName, new Message<string, string> { Key = System.Guid.NewGuid().ToString(), Value = json?.ToString() }));


        [HttpPost("json/persion")]
        public async Task<IActionResult> PostPersion([FromBody] PersionDto persion)
        {
            return Ok(await _producer.ProduceAsync(topicName, new Message<string, string>() { Key = System.Guid.NewGuid().ToString(), Value = JsonSerializer.Serialize(persion) }));
        }

    }
}