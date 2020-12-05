using System;
using System.Threading.Tasks;
using Template.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Template.API.Dto;
using Template.ORM.IRepositories;
using Template.ORM.Models.Sample;

namespace Template.API.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SampleController : Controller
    {

        protected readonly ISampleMSFEFCoreSevice _efCoreServcie;

        private readonly ISampleModelRepository _repository;

        public SampleController(ISampleMSFEFCoreSevice efCoreServcie, ISampleModelRepository repository)
        {
            _efCoreServcie = efCoreServcie;
            _repository = repository;
        }

        /// <summary>
        /// 示例接口（无参考意义）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGongZuoLTJAsync(string id) =>  Ok(await _efCoreServcie.GetSampleMSFEFCoreMethodAsync(id));


        /// <summary>
        /// 示例接口（无参考意义）
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateSampleAsync([FromBody] SampleDto sample) => Ok(await _repository.AddAsync(sample.MapTo<SampleDto, SampleModel>()));



    }
}