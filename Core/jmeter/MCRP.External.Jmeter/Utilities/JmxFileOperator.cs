using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace MCRP.External.Jmeter.Utilities
{

    /// <summary>
    /// jmx文件操作辅助
    /// </summary>
    public class JmxFileOperator : IJmxFileOperator
    {
        protected readonly JmeterOptions _jmeterOptions;

        public JmxFileOperator(IOptions<JmeterOptions> jmeterOptions)
        {
            _jmeterOptions = jmeterOptions.Value;
        }

        /// <summary>
        /// Jmeter集成的根路径
        /// </summary>
        /// <returns></returns>
        public static readonly string ROOT_PATH = Path.GetDirectoryName(typeof(Program).Assembly.Location);


        /// <summary>
        /// 检查默认的文件存储及测试结果目录
        /// </summary>
        /// <returns></returns>
        protected void CheckDefaultlBasicFolder(string id = "")
        {

            if (!Directory.Exists(_jmeterOptions.JmxRootPath))
                Directory.CreateDirectory(_jmeterOptions.JmxRootPath);
            if (!Directory.Exists(_jmeterOptions.ResultRootPath))
                Directory.CreateDirectory(_jmeterOptions.ResultRootPath);
            if (!string.IsNullOrEmpty(id))
            {
                string targetPath = Path.Combine(_jmeterOptions.JmxRootPath, id);
                if (!Directory.Exists(targetPath))
                    Directory.CreateDirectory(targetPath);
            }

        }

        /// <summary>
        /// 保存上传文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="boundary"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public async Task<bool> SaveUploadFile(string id, string boundary, Stream stream)
        {
            CheckDefaultlBasicFolder(id);
            string uploadFileName = string.Empty;
            var reader = new MultipartReader(boundary, stream);
            var section = await reader.ReadNextSectionAsync();
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);
                if (hasContentDispositionHeader)
                {
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        var trustedFileNameForDisplay = string.Empty;
                        trustedFileNameForDisplay = System.Net.WebUtility.HtmlEncode(contentDisposition.FileName.Value);
                        var _extension = Path.GetExtension(trustedFileNameForDisplay);
                        var streamContent = await FileUploadHelper.ProcessStreamFile(section, contentDisposition, 2097152);
                        if (streamContent.Length > 0)
                        {
                            uploadFileName = $"tmp{_extension}";
                            using (var targetStream = System.IO.File.Create(Path.Combine(_jmeterOptions.JmxRootPath, id, uploadFileName)))
                            {
                                await targetStream.WriteAsync(streamContent);
                            }
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            return await DealUploadFile(id, uploadFileName);
        }


        /// <summary>
        /// 处理上传上来的文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        protected async Task<bool> DealUploadFile(string id, string fileName)
        {

            return await Task<bool>.Run(async () =>
            {
                var _extension = Path.GetExtension(fileName).ToLower();
                var fileDirectory = Path.Combine(_jmeterOptions.JmxRootPath, id);
                string sourceFileName = Path.Combine(fileDirectory, fileName);
                switch (_extension)
                {
                    case ".zip":
                        //解压文件
                        await FileOperaHelper.ExtractToDirectory(sourceFileName, fileDirectory, true);
                        break;
                }

                await SetStartFile(fileDirectory);
                string targetFileName = Path.Combine(fileDirectory, "start-up.jmx");
                return  ProcessDataFilePath(targetFileName);
            });
        }


        /// <summary>
        ///统一设置测试计划的起始文件
        /// </summary>
        /// <param name="directoryPath">文件保存的目录</param>
        /// <returns></returns>
        protected async Task SetStartFile(string directoryPath)
        {
            await Task.Run(() =>
            {
                if (!Directory.Exists(directoryPath))
                    throw new DirectoryNotFoundException($"路径不存在{directoryPath}");
                string[] files = Directory.GetFiles(directoryPath);
                var query = files.Where(c => c.ToLower().Contains(".jmx"));
                if (!query.Any())
                    throw new DirectoryNotFoundException($"路径下{directoryPath},无可用的测试计划文件");
                var sourceFileName = query.FirstOrDefault();
                string targetFileName = Path.Combine(directoryPath, "start-up.jmx");
                File.Copy(sourceFileName, targetFileName, false);
                File.Delete(sourceFileName);
            });
        }


        /// <summary>
        /// 处理数据文件并校验数据文件路径
        /// </summary>
        /// <param name="jmxFilePath">jmx文件路径</param>
        /// <returns>处理结果</returns>
        protected bool ProcessDataFilePath(string jmxFilePath)
        {
            string tempFileName = $"{jmxFilePath}.tmp";

            // string fullJmxFilePath = Path.Combine(basicPath, jmxFilePath);
            string jmxfileName = Path.GetFileName(jmxFilePath);
            string fileText = File.ReadAllText(jmxFilePath);
            Regex regex = new Regex("<stringProp name=\"filename\">.*</stringProp>", RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(fileText);
            if (matches.Count > 0)
            {
                for (int i = 0; i < matches.Count; i++)
                {
                    string oldPath = matches[i].Value.Trim(' ');
                    string newPath = oldPath.Replace("<stringProp name=\"filename\">", "").Replace("</stringProp>", "").Trim(' ');
                    if (!string.IsNullOrEmpty(newPath))
                    {
                        string fileName = Path.GetFileName(newPath);
                        if (!string.IsNullOrEmpty(fileName))
                        {
                            string truePath = jmxFilePath.Replace(jmxfileName, fileName);
                            truePath = FileOperaHelper.PathTransformation(truePath);
                            if (!File.Exists(truePath))
                                throw new FileNotFoundException($"文件不存在或者路径错误或缺少文件，[{truePath}].");
                            string relativelyPath = FileOperaHelper.PathTransformation(Path.Combine("data", fileName));
                            fileText = fileText.Replace(oldPath, $"<stringProp name=\"filename\">{relativelyPath}</stringProp>");
                        }
                    }
                }
            }

            using (FileStream file = new FileStream(tempFileName, FileMode.OpenOrCreate))
            {
                byte[] bytes = System.Text.Encoding.Default.GetBytes(fileText);
                file.Write(bytes, 0, bytes.Length);
            }
            File.Delete(jmxFilePath);
            FileInfo info = new FileInfo(tempFileName);
            info.MoveTo(tempFileName.Replace(".tmp", string.Empty));
            return true;
        }
    }

}