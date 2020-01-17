using System;
using System.IO;
using Microsoft.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Linq;

namespace MCRP.External.Jmeter.Utilities
{
    /// <summary>
    /// 文件上传辅助
    /// </summary>
    public class FileUploadHelper
    {       
        /// <summary>
        /// 允许上传的文件扩展名
        /// </summary>
        /// <value></value>
         protected static string[] ALL_FILES_EXTENS = new string[] { ".zip", ".jmx" };
        
        /// <summary>
        /// 文件流处理
        /// </summary>
        /// <param name="section">MultipartSection</param>
        /// <param name="contentDisposition">ContentDispositionHeaderValue</param>
        /// <param name="sizeLimit">sizeLimit</param>
        /// <returns></returns>
        public static async Task<byte[]> ProcessStreamFile(MultipartSection section, ContentDispositionHeaderValue contentDisposition, long sizeLimit)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await section.Body.CopyToAsync(stream);
                if (stream.Length == 0)
                    throw new Exception("文件内容为空.");
                if (stream.Length > sizeLimit)
                    throw new Exception($"文件上传失败,文件的大小超出了{sizeLimit / 1048576:N1} MB限制。");
                if (!IsValidFileExtension(contentDisposition.FileName.Value, stream))
                    throw new Exception($"文件上传失败,文件格式不正确");
                return stream.ToArray();
            }
        }

        /// <summary>
        /// 判断文件类型是否合法
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsValidFileExtension(string fileName, Stream data)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
                return false;
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(ext) && ALL_FILES_EXTENS.ToList().Contains(ext);
        }

        /// <summary>
        /// 解压文件
        /// </summary>
        /// <param name="sourceFilePath"></param>
        /// <param name="targetFilePath"></param>
        /// <returns></returns>
        public static async Task<bool> ExtractFile(string sourceFilePath, string targetFilePath)
        {
            return await Task<bool>.Run(() =>
            {
                string command = string.Empty;
                string commandArguments = string.Empty;
                string ext = Path.GetExtension(sourceFilePath);
                switch (ext)
                {
                    case ".zip":
                        command = "unzip";
                        commandArguments = $" {sourceFilePath}";
                        break;
                    case ".tar":
                        command = "tar";
                        commandArguments = $" -xvf {sourceFilePath}";
                        break;
                    case ".tar.Z":
                        command = "tar";
                        commandArguments = $" -xZf {sourceFilePath}";
                        break;
                    case ".tar.gz":
                    case ".tgz":
                        command = "tar";
                        commandArguments = $" -xzf {sourceFilePath}";
                        break;
                    case ".rar":
                        command = "unrar";
                        commandArguments = $" e {sourceFilePath}";
                        break;
                }
                return ProcessStartOpearHelper.ProcessRun(command, commandArguments);
            });

        }

    }
    
}