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

        protected static string[] ALL_FILES_EXTENS = new string[] {".zip",".rar",".tar",".zipx"};

        public static async Task<byte[]> ProcessStreamdFile(MultipartSection section, ContentDispositionHeaderValue contentDisposition, ModelStateDictionary modelState,long sizeLimit)
        {
            try
            {          
                using (MemoryStream stream = new MemoryStream())
                {
                    await section.Body.CopyToAsync(stream);
                    if (stream.Length == 0)
                        modelState.AddModelError("文件上传失败", "文件内容为空");
                    if (stream.Length > sizeLimit)
                        modelState.AddModelError("文件上传失败", $"文件的大小超出了{sizeLimit / 1048576:N1} MB限制。");
                   if(!IsValidFileExtension(contentDisposition.FileName.Value,stream))
                        modelState.AddModelError("文件上传失败", $"文件格式不正确");
                    return stream.ToArray();
                }
            }
            catch (System.Exception ex)
            {
                modelState.AddModelError("文件上传失败", ex.Message);
            }
            return new byte[0];
        }

        public static bool IsValidFileExtension(string fileName, Stream data)
        {
            if (string.IsNullOrEmpty(fileName) || data != null || data.Length == 0)
                return false;
            var ext = Path.GetExtension(fileName).ToLowerInvariant();
            return !string.IsNullOrEmpty(ext) && !ALL_FILES_EXTENS.ToList().Contains(ext);
        }
    }

}