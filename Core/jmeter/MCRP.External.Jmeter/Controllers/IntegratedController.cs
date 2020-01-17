using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MCRP.External.Jmeter.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace MCRP.External.Jmeter.Controllers
{
    [ApiController]
    [Route("[controller]")]
   public class IntegratedController:ControllerBase
    {           

        /// <summary>
        /// 
        /// </summary>
        protected readonly IJmeterPlanActuator _actuator;


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="actuator"></param>
        public IntegratedController(IJmeterPlanActuator actuator)
        {
            _actuator = actuator;
        }

        /// <summary>
        /// 获取测试报告
        /// </summary>
        /// <param name="id">执行计划ID</param>
        /// <returns>测试报告文字内容</returns>
        [HttpGet("/{id}")]
        public async Task<ActionResult> GetJmeterResult(string id)
        {        
            try
            {            
                CommandOption option = new CommandOption() { Id = id, ResultType = JmeterResultType.Ordinary, RemoteExecute = true };
                return Ok(new {code="1",data=await _actuator.ReadOrdinaryResult(option)});               
            }
            catch (Exception ex)
            {
                return BadRequest(new { code = "-1", message = ex.Message });
            }    

        }


        /// <summary>
        /// 执行测试计划并返回结果
        /// </summary>
        /// <param name="id">上传接口的返回的ID</param>       
        /// <returns>返回测试报告，异常时返回异常原因</returns>
        [HttpPost("ExecuteAndReturnResult/{id}")]
        public async Task<ActionResult> PostExecuteAndReturnResult(string id)
        {
            List<string[]> result = new List<string[]>();
            try
            {    
                return  new JsonResult(new { code = "1", message = "ok", data = await ExecuteProcessAndReadResult(id) });          
            } 
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });
            }         
        }


        /// <summary>
        /// 
        /// </summary>      
        /// <param name="id"></param>
        private async Task<List<string[]>> ExecuteProcessAndReadResult(string id)
        {            
            CommandOption option = new CommandOption() { Id = id, ResultType = JmeterResultType.Ordinary, RemoteExecute = true };
            await _actuator.Execute(option);
            return  await _actuator.ReadOrdinaryResult(option);
        }

        /// <summary>
        /// 执行测试计划
        /// </summary>
        /// <param name="id">上传接口返回编号</param>
        /// <returns>返回执行结果，不返回测试报告</returns>
        [HttpPost("Execut/{id}")]
        public async Task<ActionResult> Execut(string id)
        {
            try
            {
                CommandOption option = new CommandOption() { Id = id, ResultType = JmeterResultType.Ordinary, RemoteExecute = true };
                await _actuator.Execute(option);
               return  Ok(new { code = "1", message = "ok"});
            }
            catch (System.Exception ex)
            {                
              return BadRequest(new { code = "-1", message = ex.Message });
            }          
        }
        
    }

    
}