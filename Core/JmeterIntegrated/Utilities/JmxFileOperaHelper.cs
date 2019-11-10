using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace JmeterIntegrated.Utilities
{
    public static class JmxFileOperaHelper
    {
        /// <summary>
        /// 查找jmx文件
        /// </summary>
        /// <param name="directoryPath">上传文件之后解压文件目录</param>
        /// <returns>jmx文件上的完整路径</returns>
        public static async Task<string> GetJmxFilePath(string directoryPath)
        {
            return await Task<string>.Run(() =>
            {
                if (!Directory.Exists(directoryPath))
                    throw new DirectoryNotFoundException($"路径不存在{directoryPath}");
                string[] files = Directory.GetFiles(directoryPath);
                var jmxFiles = files.Where(c => c.ToLower().EndsWith(".jmx"));
                if (!jmxFiles.Any())
                    throw new FileNotFoundException($"压缩包上上传的文件内没有找到可用的jmx文件");
                return Path.Combine(directoryPath, jmxFiles.First());
            });
        }

        public static async Task<bool> ProcessDataFilePath(string jmxFilePath)
        {


            File.ReadAllText(jmxFilePath);



            return false;
        }

    }
}