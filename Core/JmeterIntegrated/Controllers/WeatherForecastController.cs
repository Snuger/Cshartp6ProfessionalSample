using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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

        /// <summary>
        /// 执行测试
        /// </summary>
        /// <param name="jmx">jmx文件(不需要后缀)</param>
        /// <returns></returns>
        [HttpPost("Execution/{jmx}")]
        public async Task<ActionResult>PostExecution(string jmx)
        {
            string id = System.Guid.NewGuid().ToString();
            string jmeterRoot = @"D:\Program Files\jmeter-5.1.1_2\bin\";
            string jmxRoot = Path.Combine(jmeterRoot, "plan");
            string jmeterPath = Path.Combine(jmeterRoot, "jmeter.bat");
            string jmxPath = Path.Combine(jmxRoot,$"{jmx}.jmx");
            string logPath = Path.Combine(jmxRoot,$"{id}.jtl");
            JsonResult result = null;
            try
            {

                if (System.IO.File.Exists(logPath))
                    System.IO.File.Delete(logPath);

                if (!System.IO.File.Exists(jmeterPath))
                    throw new Exception($"{jmeterPath}文件不存在");

               var finished= await Task<bool>.Run(()=> {
                   bool _reuslt = false;
                   using (Process process = new Process()) {
                       process.StartInfo.UseShellExecute = false;
                       process.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                       process.StartInfo.RedirectStandardOutput =false;  //由调用程序获取输出信息
                       process.StartInfo.CreateNoWindow = false;
                       process.StartInfo.FileName = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Windows\System32\cmd.exe";
                       process.Start();
                       process.StandardInput.WriteLine($"\"{jmeterPath}\" -n -t \"{jmxPath}\" -l \"{logPath}\"");                            
                   }
                   _reuslt = true;               
                   return _reuslt;
                });

                if(finished)
                    result = new JsonResult(new { id = id, code = 1 });
            }
            catch (Exception ex)
            {
                result = new JsonResult(new {id=id,code=-1,message=ex.Message });
            }     
            return result;
        }

        /// <summary>
        /// 获取执行结果
        /// </summary>
        /// <param name="id">执行任务ID</param>
        /// <returns></returns>
        [HttpPost("QueryJmeterResult/{id}")]
        public async Task<ActionResult> QueryJmeterResult(string id)
        {

            string jmeterRoot = @"D:\Program Files\jmeter-5.1.1_2\bin\";
            string jmxRoot = Path.Combine(jmeterRoot, "plan");
            string logPath = Path.Combine(jmxRoot,$"{id}.jtl");
            JsonResult result = null;
            try
            {
                if (!System.IO.File.Exists(logPath))
                    throw new Exception($"{logPath}文件不存在，执行计划正常在执行或者未执行完成.");
                      

             var _jmeterResult= await Task<List<string[]>>.Run(() =>
                {
                    List<string[]> jmeterResult = new List<string[]>();
                    string line;
                    int counter = 0;
                    System.IO.StreamReader file = new System.IO.StreamReader(logPath);
                    while ((line = file.ReadLine()) != null)
                    {
                        if (counter != 0)
                            jmeterResult.Add(line.Split(","));
                        counter++;
                    }
                    return jmeterResult;
                });

                result= new JsonResult(new { code = -1, message = "ok",data=_jmeterResult });
            }
            catch (Exception ex)
            {
                result = new JsonResult(new { code = -1, message = ex.Message });
            }
            return result;
        }



        [HttpPost("ExecuteAndReturnResult")]
        public async Task<ActionResult>PostExecuteAndReturnResult(string jmx)
        {
            List<string[]> result=new List<string[]>();
            try
            {
                string id = System.Guid.NewGuid().ToString();
                result= await ExecuteProcessAndReadResult(jmx, id);

            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });
                
            }
            return new JsonResult(result);
        }

    
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jmx"></param>
        /// <param name="id"></param>
        private async Task<List<string[]>> ExecuteProcessAndReadResult(string jmx,string id) {

            string jmeterRoot = @"D:\Program Files\jmeter-5.1.1_2\bin\";
            string jmxRoot = Path.Combine(jmeterRoot, "plan");
            string jmeterPath = Path.Combine(jmeterRoot, "jmeter.bat");
            string jmxPath = Path.Combine(jmxRoot, $"{jmx}.jmx");
            string logPath = Path.Combine(jmxRoot, $"{id}.jtl");
            await  Task.Run(()=> {               
                Process process = new Process();
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                process.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                process.StartInfo.RedirectStandardError = true;               
                process.StartInfo.FileName = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Windows\System32\cmd.exe";
                using (process) {
                    process.Start();
                    process.StandardInput.WriteLine($"\"{jmeterPath}\" -n -t \"{jmxPath}\" -l \"{logPath}\"");                    
                    process.WaitForExit(120);
                    process.Close();                  
                }               
            });

            return await Task<List<string[]>>.Run(()=>{                 
                 List<string[]> jmeterResult = new List<string[]>();
                    string line;
                    int counter = 0,times=0;
                    while ((!System.IO.File.Exists(logPath)&&times<30)||isUsed(logPath)) {
                        Thread.Sleep(2000);
                        times += 1;
                    }
                    if (System.IO.File.Exists(logPath))
                    {
                        System.IO.StreamReader file = new System.IO.StreamReader(logPath);
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

        protected bool isUsed(string path){
            bool isUsed=false;
            try
            {
                FileStream stream=new FileStream(path,FileMode.Open,FileAccess.Read,FileShare.None);
                stream.Close();
                
            }
            catch (System.Exception)
            {
                isUsed=true;
            }
            return isUsed;
        }
    }
}
