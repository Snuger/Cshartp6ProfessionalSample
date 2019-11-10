
using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Linq;

namespace JmeterIntegrated.Utilities
{
    public class FileOperaHelper
    {
        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="zipPath">压缩名文件路径</param>
        /// <param name="directoryPath">目标目录</param>
        /// <param name="deleteSource">成功解压后是否删除原文件</param>
        /// <returns>解压结果(true/false)</returns>
        public static async Task<bool> ExtractToDirectory(string zipPath, string directoryPath, bool deleteSource = false)
        {
            return await Task<bool>.Run(() =>
            {
                if (!File.Exists(zipPath))
                    throw new FieldAccessException($"{zipPath}压缩文件不存在.");
                ZipFile.ExtractToDirectory(zipPath, directoryPath);
                if (deleteSource)
                    File.Delete(zipPath);
                return true;
            });
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="directoryPath">目录</param>
        /// <param name="zipPath">压缩文件名</param>
        /// <returns>true/false</returns>
        public static async Task<bool> CreateFromDirectory(string directoryPath, string zipPath)
        {
            return await Task<bool>.Run(() =>
            {
                if (!Directory.Exists(directoryPath))
                    throw new DirectoryNotFoundException($"路径不存在{directoryPath}");
                ZipFile.CreateFromDirectory(directoryPath, zipPath);
                return true;
            });
        }


        public static async Task<string> GetJmxFilePath(string directoryPath)
        {
            return await Task<string>.Run(() =>
            {

                if (!Directory.Exists(directoryPath))
                    throw new DirectoryNotFoundException($"路径不存在{directoryPath}");
                string[] files = Directory.GetFiles(directoryPath);
                var jmxFiles = files.Where(c => c.EndsWith(".jmx"));
                if (!jmxFiles.Any())
                    throw new FileNotFoundException($"压缩包上上传的文件内没有找到可用的jmx文件");
                return Path.Combine(directoryPath, jmxFiles.First());
            });
        }
    }

}