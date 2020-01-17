using System;
using System.IO;
using System.Runtime.InteropServices;

namespace MCRP.External.Jmeter.Utilities
{
    /// <summary>
    /// Jmeter基本参数配置项目
    /// </summary>
    public class CommandOption
    {
        /// <summary>
        /// 执行计划ID
        /// </summary>
         public string  Id { get; set; }

        /// <summary>
        /// 远程执行
        /// </summary>     
         public bool  RemoteExecute { get; set; }

        /// <summary>
        /// 测试结果类型
        /// </summary>       
         public JmeterResultType ResultType{get;set;}

          
    }
    
}