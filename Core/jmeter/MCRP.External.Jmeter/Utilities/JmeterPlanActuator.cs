using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace MCRP.External.Jmeter.Utilities
{
    /// <summary>
    /// 测试计划执行器
    /// </summary>
   public class  JmeterPlanActuator:IJmeterPlanActuator
   {
       /// <summary>
       /// 启动进程的对象
       /// </summary>
        protected  ProcessStartInfo _process;  

        /// <summary>
        /// jmeter基础配置
        /// </summary>
        /// <value></value>
         protected  readonly JmeterOptions _jmeterOptions;    


        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <param name="options"></param>
        public JmeterPlanActuator(ProcessStartInfo process,IOptions<JmeterOptions> options)
        {
           _process=process;  
           _jmeterOptions=options.Value;         
        }

        /// <summary>
        /// 执行测试计划
        /// </summary>
        /// <param name="option">计划参数</param>
        /// <returns></returns>
        public async Task Execute(CommandOption option)
        {
            string aguments = BuildArguments(option);
            await Task.Run(() =>
            {           
                if(!File.Exists(_jmeterOptions.FileName))
                    throw new FileNotFoundException($"无法找到可执行Jmeter的启动文件{_jmeterOptions.FileName}");
                if (string.IsNullOrWhiteSpace(_process.FileName))                    
                    _process.FileName = _jmeterOptions.FileName;
                _process.Arguments = aguments;
                _process.RedirectStandardOutput = false;
                var proc = Process.Start(_process);            
                proc.WaitForExit();
                if (!proc.HasExited)
                {
                    proc.Kill();
                }              
            });
        }

      /// <summary>
      /// 返回测试结果
      /// </summary>
      /// <param name="option">参数配置</param>
      /// <returns>详细的测试结果</returns>
        public async Task<List<string[]>> ReadOrdinaryResult(CommandOption option)
        {
            return await Task<List<string[]>>.Run(() =>
            {
               List<string[]> jmeterResult = new List<string[]>();
                string resultFileName = Path.Combine(_jmeterOptions.ResultRootPath, option.Id, _jmeterOptions.ResultSuffix);
                if (!File.Exists(resultFileName))
                    throw new FileNotFoundException($"测试计划执行失败,或报告ID错误找不到结果{resultFileName}");
                string line = string.Empty;
                int counter = 0;
                System.IO.StreamReader file = new System.IO.StreamReader(resultFileName);
                while ((line = file.ReadLine()) != null)
                {
                    if (counter != 0)
                        jmeterResult.Add(line.Split(","));
                    counter++;
                }
                return jmeterResult;
            });
        }


        /// <summary>
        /// 构建执行命令
        /// </summary>
        /// <param name="option"></param>
        /// <returns>命令</returns>
        protected string BuildArguments(CommandOption option){           
          string jmxfileName=Path.Combine(_jmeterOptions.JmxRootPath,option.Id,"start-up.jmx");
          if(!File.Exists(jmxfileName))
                throw new FileNotFoundException($"文件不存在或者路径异常{jmxfileName}");
          string resultFileName=Path.Combine(_jmeterOptions.ResultRootPath,option.Id,_jmeterOptions.ResultSuffix);
          string executeType=option.RemoteExecute?"-r":"";          
          return $" -n -t {jmxfileName}  {executeType} -n {resultFileName}";
        }
   }    
}