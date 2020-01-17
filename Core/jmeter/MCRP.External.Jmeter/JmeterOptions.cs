using System;



namespace MCRP.External.Jmeter
{
    /// <summary>
    /// Jmeter配置
    /// </summary>
    public class JmeterOptions
    {
        /// <summary>
        /// 可执行文件路径
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 测试计划文件存储路径
        /// </summary>
        public string JmxRootPath { get; set; }

        /// <summary>
        /// 测试结果存储路径
        /// </summary>
        public string ResultRootPath { get; set; }


        /// <summary>
        /// html报告文件路径
        /// </summary>
        public string ReportRootPath { get; set; }

        /// <summary>
        /// 测试结果文件后缀
        /// </summary>
        public string ResultSuffix { get; set; }

        /// <summary>
        /// 单个文件的最大上限
        /// </summary>
        public int SingleFileMaxSize { get; set; }

    }
}