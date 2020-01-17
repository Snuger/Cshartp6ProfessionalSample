using System;
using System.Threading.Tasks;
using MCRP.External.Jmeter.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace MCRP.External.Jmeter.Controllers
{

    [ApiController]    
    [Route("[controller]")]
    public class FileUploadController:ControllerBase
    {

        protected readonly IJmxFileOperator _operator;

        public FileUploadController(IJmxFileOperator jmxFileOperator)
        {
            _operator = jmxFileOperator;
        }


        /// <summary>
        /// 接收上传的文件并进行处理
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> PostFile()
        {
            try
            { 
                string id = System.Guid.NewGuid().ToString();
                string savePath = $"{id}";
                if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
                {
                    ModelState.AddModelError("File", "非法请求或请求的格式不正确.");
                    return BadRequest(ModelState);
                }
                MediaTypeHeaderValue headerValue = MediaTypeHeaderValue.Parse(Request.ContentType);
                var boundary = MultipartRequestHelper.GetBoundary(headerValue, 2097152);
                if (string.IsNullOrEmpty(boundary))
                {
                    ModelState.AddModelError("ContentType", "Missing content-type boundary.");
                    return BadRequest(ModelState);
                }               
                await _operator.SaveUploadFile(id, boundary, HttpContext.Request.Body);                
                return Ok(new { code = "1", message = "上传成功", id=id });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { code = "-1", message = ex.Message});
            }
       }

    }
    
}