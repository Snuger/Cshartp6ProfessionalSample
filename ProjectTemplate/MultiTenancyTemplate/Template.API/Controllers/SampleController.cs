 
using MCRP.MultiTenancy.ORM.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Template.ORM.Models;

namespace Template.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController:ControllerBase
    {
        private readonly IEfCoreRepository<SampleModel, string> _repository;
        public SampleController(IEfCoreRepository<SampleModel, string>  repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var b= await _repository.GetAsync(o => o.Id == "1");
            if(b==null)
            {
                return Ok("null");
            }
            return Ok(b.TenantId.ToString());
        }

        [HttpGet("add")]
        public async Task<IActionResult> Post()
        {
            var b= await _repository.InsertAsync(new SampleModel
            { 
             Name="test"
            },true);
            return Ok(b.Id);
        }

        [HttpGet("delete")]
        public async Task<IActionResult> Delete(string id
            )
        {
            var b = await _repository.GetAsync(o => o.Id == id);
            if (b == null)
            {
                return Ok("null");
            }
            await _repository.DeleteAsync(b,true);
            return Ok("complete");
        }
    }
}
