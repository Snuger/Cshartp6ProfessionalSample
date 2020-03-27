using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HostConfigSamples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerConfigController : ControllerBase
    {
        protected readonly JmeterOptions _options;

        public CustomerConfigController(IOptions<JmeterOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new { code = 1, data = _options });
        }

    }
}