using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Hosting;
using JmeterIntegrated.Utilities;

namespace JmeterIntegrated.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取测试报告
        /// </summary>
        /// <param name="id">执行计划ID</param>
        /// <returns>测试报告文字内容</returns>
        [HttpPost("QueryJmeterResult/{id}")]
        public async Task<ActionResult> QueryJmeterResult(string id)
        {
            List<string[]> result = new List<string[]>();
            try
            {
                result = await Task<List<string[]>>.Run(() =>
               {
                   List<string[]> jmeterResult = new List<string[]>();
                   if (System.IO.File.Exists($"result/{id}.csv"))
                   {
                       string line = string.Empty;
                       int counter = 0;
                       System.IO.StreamReader file = new System.IO.StreamReader($"result/{id}.csv");
                       while ((line = file.ReadLine()) != null)
                       {
                           if (counter != 0)
                               jmeterResult.Add(line.Split(","));
                           counter++;
                       }
                   }
                   return jmeterResult;
               });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });
            }
            return new JsonResult(new { code = "1", message = "成功", data = result });

        }


        /// <summary>
        /// 执行测试计划并返回结果
        /// </summary>
        /// <param name="jmx">执行测试计划的jmx文件(不包含后缀)</param>
        /// <returns>返回测试报告，异常时返回异常原因</returns>
        [HttpPost("ExecuteAndReturnResult")]
        public async Task<ActionResult> PostExecuteAndReturnResult(string jmx = "samples")
        {
            List<string[]> result = new List<string[]>();
            try
            {
                string id = System.Guid.NewGuid().ToString();
                result = await ExecuteProcessAndReadResult(jmx, id);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });

            }
            return new JsonResult(new { code = "1", message = "ok", data = result });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jmx"></param>
        /// <param name="id"></param>
        private async Task<List<string[]>> ExecuteProcessAndReadResult(string jmx, string id)
        {
            string _id = System.Guid.NewGuid().ToString();
            string jmeterPath = "/usr/local/jmeter/bin/jmeter";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                jmeterPath = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Program Files\jmeter\bin\jmeter.bat";
            }
            else
            {
                jmeterPath = "/usr/local/jmeter/bin/jmeter";
            }
            return await Task<List<string[]>>.Run(() =>
            {
                List<string[]> jmeterResult = new List<string[]>();
                ProcessStartInfo process = new ProcessStartInfo(jmeterPath, $" -n -t jmx/{jmx}.jmx -r -l result/{_id}.csv") { RedirectStandardOutput = true };
                var proc = Process.Start(process);
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        string msg = sr.ReadLine();
                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                    if (sr.EndOfStream)
                    {
                        if (System.IO.File.Exists($"result/{_id}.csv"))
                        {
                            string line = string.Empty;
                            int counter = 0;
                            System.IO.StreamReader file = new System.IO.StreamReader($"result/{_id}.csv");
                            while ((line = file.ReadLine()) != null)
                            {
                                if (counter != 0)
                                    jmeterResult.Add(line.Split(","));
                                counter++;
                            }
                        }
                    }
                }
                return jmeterResult;
            });
        }

        /// <summary>
        /// 执行测试计划
        /// </summary>
        /// <param name="jmx">执行测试计划的jmx文件(不包含后缀)</param>
        /// <returns>返回执行结果，不返回测试报告</returns>
        [HttpPost("ExecutionPlan/{jmx}")]
        public async Task<ActionResult> BoastPlatformJmeter(string jmx = "samples")
        {
            string _id = System.Guid.NewGuid().ToString();
            string jmeterPath = "/usr/local/jmeter/bin/jmeter";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                jmeterPath = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Program Files\jmeter\bin\jmeter.bat";
            }
            else
            {
                jmeterPath = "/usr/local/jmeter/bin/jmeter";
            }
            var result = await Task<StringBuilder>.Run(() =>
             {
                 StringBuilder message = new StringBuilder();
                 ProcessStartInfo process = new ProcessStartInfo(jmeterPath, $" -n -t jmx/{jmx}.jmx -r -l result/{_id}.csv") { RedirectStandardOutput = true };
                 var proc = Process.Start(process);
                 if (proc == null)
                 {
                     message.Append("error");
                     return message;
                 }
                 using (var sr = proc.StandardOutput)
                 {
                     while (!sr.EndOfStream)
                     {
                         message.Append(sr.ReadLine());
                     }
                     if (!proc.HasExited)
                     {
                         proc.Kill();
                     }
                 }
                 return message;
             });
            return new JsonResult(new { fileName = jmx, id = _id, result = (result.ToString() != "error"), message = result.ToString() });
        }


        [HttpPost("upload")]
        public async Task<ActionResult> UpLoadFile()
        {  
             string id=System.Guid.NewGuid().ToString();
            if (string.IsNullOrEmpty(Request.ContentType) || Request.ContentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) < 0)
            {
                ModelState.AddModelError("File", "请求不合法.");
                return BadRequest(ModelState);
            }
            MediaTypeHeaderValue headerValue = MediaTypeHeaderValue.Parse(Request.ContentType);
            var boundary = HeaderUtilities.RemoveQuotes(headerValue.Boundary).Value;
            if (string.IsNullOrEmpty(boundary))
            {
                ModelState.AddModelError("ContentType", "Missing content-type boundary.");
                return BadRequest(ModelState);
            }

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    if (contentDisposition != null && contentDisposition.DispositionType.Equals("form-data"))
                    {
                        var untrustedFileNameForStorage = string.Empty;
                        var trustedFileNameForDisplay = string.Empty;
                        untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        var streamContent = await Task<byte[]>.Run(() =>
                        {
                            try
                            {
                                using (var memorystream = new MemoryStream())
                                {                                    
                                     section.Body.CopyToAsync(memorystream);   
                                    if (memorystream.Length == 0)
                                    {
                                        ModelState.AddModelError("File", "文件内容为空");
                                    }
                                    if (memorystream.Length > 2097152)
                                    {
                                        var megabyteSizeLimit = 2097152 / 1048576;
                                        ModelState.AddModelError("File", $"The file exceeds {megabyteSizeLimit:N1} MB.");
                                    }

                                    return memorystream.ToArray();
                                }
                            }
                            catch (System.Exception ex)
                            {
                                ModelState.AddModelError("File", $"{ex.Message}");
                            }
                            return new byte[0];

                        });

                        if(!ModelState.IsValid){
                            return BadRequest(ModelState);
                        }

                        using (var targetStream=System.IO.File.Create($"jmx/{id}.jmx"))
                        {
                             await   targetStream.WriteAsync(streamContent);                            
                        }
                    }

                }

             section =await reader.ReadNextSectionAsync();

            }
             return Ok(new {code="1",message="上传成功",path=$"jmx/{id}.jmx"});

        }

        /// <summary>
        /// 接收上传的文件并进行处理
        /// </summary>
        /// <returns></returns>
        [HttpPost("uploadfile")]
        public async Task<ActionResult>UpLoadAndProcessFile(){
            string id=System.Guid.NewGuid().ToString();
            string _extension=string.Empty;
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType))
            {
                ModelState.AddModelError("File", "非法请求或请求的格式不正确.");
                return BadRequest(ModelState);
            }
            MediaTypeHeaderValue headerValue = MediaTypeHeaderValue.Parse(Request.ContentType);
            var boundary =MultipartRequestHelper.GetBoundary(headerValue,2097152);
            if (string.IsNullOrEmpty(boundary))
            {
                ModelState.AddModelError("ContentType", "Missing content-type boundary.");
                return BadRequest(ModelState);
            }

            var reader = new MultipartReader(boundary, HttpContext.Request.Body);
            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        //var untrustedFileNameForStorage = string.Empty;
                        var trustedFileNameForDisplay = string.Empty;
                        //untrustedFileNameForStorage = contentDisposition.FileName.Value;
                        trustedFileNameForDisplay = WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        _extension=Path.GetExtension(trustedFileNameForDisplay);
                        var streamContent=await FileUploadHelper.ProcessStreamdFile(section,contentDisposition,ModelState,2097152);
                        
                        if(!ModelState.IsValid){
                            return BadRequest(ModelState);
                        }

                        using (var targetStream=System.IO.File.Create($"jmx/{id}{_extension}"))
                        {
                             await   targetStream.WriteAsync(streamContent);                            
                        }
                    }
                }

             section =await reader.ReadNextSectionAsync();
            }
             return Ok(new {code="1",message="上传成功",path=$"jmx/{id}{_extension}"});

        }

    }
}