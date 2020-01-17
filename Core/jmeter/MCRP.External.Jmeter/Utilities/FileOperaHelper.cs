using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MCRP.External.Jmeter.Utilities
{

 /// <summary>
 /// 文件操作辅助
 /// </summary>
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
               
                using(var zip=ZipFile.Open(zipPath,ZipArchiveMode.Read,Encoding.GetEncoding("GBK")))
                {
                     zip.ExtractToDirectory(directoryPath,true);                    
                }                
               
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


        /// <summary>
        /// 文件路径转换
        /// </summary>
        /// <param name="sourcePath">原文件路径</param>
        /// <returns>返回实际环境对应的路径</returns>
        public static string PathTransformation(string sourcePath){          
                string _path=string.Empty;                 
                   if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)||RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    _path=sourcePath.Replace("/","\\");
                   if(RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                        _path=sourcePath.Replace("\\","/");  
                 return _path;
        }
    }

}