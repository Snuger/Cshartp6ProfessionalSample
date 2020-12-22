using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataCenterCollector.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace DataCenterCollector.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TopicController : ControllerBase
    {

        private readonly IAdminClient _client;

        public TopicController(IAdminClient client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopicAsync([FromBody] IEnumerable<TopicSpecification> topics)
        {
            await _client.CreateTopicsAsync(topics);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTopicAsync([FromBody] List<string> topics)
        {
            await _client.DeleteTopicsAsync(topics);
            return Ok();
        }

        [HttpGet("groups/all")]
        public IActionResult GetGroupsAsync()
        {
            return Ok(_client.ListGroups(TimeSpan.FromSeconds(10)));
        }


        [HttpGet("brkoker/all")]
        public IActionResult GetActionMethodName()
        {
            return Ok(_client.GetMetadata(TimeSpan.FromSeconds(10))?.Brokers);

        }

    }
}