using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http;
using Microsoft.Net.Http.Headers;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace JmeterIntegrated.Utilities
{
    public class FileUploadHelper
    {

        protected static string[] ALL_FILES_EXTENS = new string[] { ".zip" };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="section"></param>
        /// <param name="contentDisposition"></param>
        /// <param name="modelState"></param>
        /// <param name="sizeLimit"></param>
        /// <returns></returns>
        public static async Task<byte[]> ProcessStreamFile(MultipartSection section, ContentDispositionHeaderValue contentDisposition, ModelStateDictionary modelState, long sizeLimit)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                await section.Body.CopyToAsync(stream);
                if (stream.Length == 0)
                    modelState.AddModelError("message", "文件上传失败,文件内容为空");
                if (stream.Length > sizeLimit)
                    modelState.AddModelError("message", $"文件上传失败,文件的大小超出了{sizeLimit / 1048576:N1} MB限制。");
                if (!IsValidFileExtension(contentDisposition.FileName.Value, stream))
                    modelState.AddModelError("message", $"文件上传失败,文件格式不正确");
                return stream.ToArray();
            }
        }

        public static bool IsValidFileExtension(string fileName, Stream data)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
                return false;
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(ext) && ALL_FILES_EXTENS.ToList().Contains(ext);
        }

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