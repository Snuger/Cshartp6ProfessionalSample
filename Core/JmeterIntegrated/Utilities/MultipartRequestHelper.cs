using System;
using System.IO;
using Microsoft.Net.Http.Headers;

namespace JmeterIntegrated.Utilities
{

    public static class MultipartRequestHelper
    {
        /// <summary>
        /// 判断是否是文件上传语法
        /// </summary>
        /// <param name="contentType">Request.ContentType</param>
        /// <returns>true或false</returns>
        public static bool IsMultipartContentType(string contentType) => !string.IsNullOrEmpty(contentType) && contentType.IndexOf("multipart/",StringComparison.OrdinalIgnoreCase) >=0;

        public static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary).Value;
            if (string.IsNullOrWhiteSpace(boundary))            
                throw new InvalidDataException("Missing content-type boundary.");
            if (boundary.Length > lengthLimit)           
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");
            return boundary;
        }

        //文件流方式上传
        // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
        /// <summary>
        /// 判断是否有文件被上传
        /// </summary>
        /// <param name="contentDisposition">请求内容头描述</param>
        /// <returns>true/false</returns>
        public static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition) => contentDisposition != null&& contentDisposition.DispositionType.Equals("form-data") && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)|| !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));

    }


}