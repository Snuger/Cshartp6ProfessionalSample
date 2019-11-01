using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text;

namespace JmeterIntegrated.Controllers
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
        public async Task<ActionResult> PostExecuteAndReturnResult(string jmx="samples")
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
                   while(!sr.EndOfStream)
                   {
                       string msg=sr.ReadLine();                      
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
        public async Task<ActionResult> BoastPlatformJmeter(string jmx="samples")
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
    }
}