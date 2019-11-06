using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;

namespace JmeterHosts.Controllers
{


    /// <summary>
    /// Jmeter���ɽӿ�
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class IntegratedController : ControllerBase
    {

        /// <summary>
        /// ��ȡ���Ա���
        /// </summary>
        /// <param name="id">ִ�мƻ�ID</param>
        /// <returns>���Ա�����������</returns>
        [HttpPost("QueryJmeterResult/{id}")]
        public async Task<ActionResult> QueryJmeterResult(string id)
        {
            List<string[]> result = new List<string[]>();
            try
            {
                result = await Task<List<string[]>>.Run(() =>
                {
                    List<string[]> jmeterResult = new List<string[]>();
                    if (System.IO.File.Exists($"result/{id}.csv"))
                    {
                        string line = string.Empty;
                        int counter = 0;
                        System.IO.StreamReader file = new System.IO.StreamReader($"result/{id}.csv");
                        while ((line = file.ReadLine()) != null)
                        {
                            if (counter != 0)
                                jmeterResult.Add(line.Split(","));
                            counter++;
                        }
                    }
                    return jmeterResult;
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });
            }
            return new JsonResult(new { code = "1", message = "�ɹ�", data = result });

        }


        /// <summary>
        /// ִ�в��Լƻ������ؽ��
        /// </summary>
        /// <param name="jmx">ִ�в��Լƻ���jmx�ļ�(��������׺)</param>
        /// <returns>���ز��Ա��棬�쳣ʱ�����쳣ԭ��</returns>
        [HttpPost("ExecuteAndReturnResult")]
        public async Task<ActionResult> PostExecuteAndReturnResult(string jmx = "samples")
        {
            List<string[]> result = new List<string[]>();
            try
            {
                string id = System.Guid.NewGuid().ToString();
                result = await ExecuteProcessAndReadResult(jmx, id);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = "-1", message = ex.Message });

            }
            return new JsonResult(new { code = "1", message = "ok", data = result });
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="jmx"></param>
        /// <param name="id"></param>
        private async Task<List<string[]>> ExecuteProcessAndReadResult(string jmx, string id)
        {
            string _id = System.Guid.NewGuid().ToString();
            string jmeterPath = "/usr/local/jmeter/bin/jmeter";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                jmeterPath = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Program Files\jmeter\bin\jmeter.bat";
            }
            else
            {
                jmeterPath = "/usr/local/jmeter/bin/jmeter";
            }
            return await Task<List<string[]>>.Run(() =>
            {
                List<string[]> jmeterResult = new List<string[]>();
                ProcessStartInfo process = new ProcessStartInfo(jmeterPath, $" -n -t jmx/{jmx}.jmx -r -l result/{_id}.csv") { RedirectStandardOutput = true };
                var proc = Process.Start(process);
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        string msg = sr.ReadLine();
                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                    if (sr.EndOfStream)
                    {
                        if (System.IO.File.Exists($"result/{_id}.csv"))
                        {
                            string line = string.Empty;
                            int counter = 0;
                            System.IO.StreamReader file = new System.IO.StreamReader($"result/{_id}.csv");
                            while ((line = file.ReadLine()) != null)
                            {
                                if (counter != 0)
                                    jmeterResult.Add(line.Split(","));
                                counter++;
                            }
                        }
                    }
                }
                return jmeterResult;
            });
        }

        /// <summary>
        /// ִ�в��Լƻ�
        /// </summary>
        /// <param name="jmx">ִ�в��Լƻ���jmx�ļ�(��������׺)</param>
        /// <returns>����ִ�н���������ز��Ա���</returns>
        [HttpPost("ExecutionPlan/{jmx}")]
        public async Task<ActionResult> BoastPlatformJmeter(string jmx = "samples")
        {
            string _id = System.Guid.NewGuid().ToString();
            string jmeterPath = "/usr/local/jmeter/bin/jmeter";
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                jmeterPath = $@"{Environment.GetEnvironmentVariable("systemdrive")}\Program Files\jmeter\bin\jmeter.bat";
            }
            else
            {
                jmeterPath = "/usr/local/jmeter/bin/jmeter";
            }
            var result = await Task<StringBuilder>.Run(() =>
            {
                StringBuilder message = new StringBuilder();
                ProcessStartInfo process = new ProcessStartInfo(jmeterPath, $" -n -t jmx/{jmx}.jmx -r -l result/{_id}.csv") { RedirectStandardOutput = true };
                var proc = Process.Start(process);
                if (proc == null)
                {
                    message.Append("error");
                    return message;
                }
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        message.Append(sr.ReadLine());
                    }
                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
                return message;
            });
            return new JsonResult(new { fileName = jmx, id = _id, result = (result.ToString() != "error"), message = result.ToString() });
        }

        //[HttpPost("Upload")]
        //public async Task<ActionResult>UploadFile()
        //{
        //    string _filderPath = "jmx";
        //    string guid=System.Guid.NewGuid().ToString();
        //   // FormFileCollection files=HttpContext.Request.Form.Files;

        //    return await Task<JsonResult>.Run(() =>
        //    {                

        //        if (!Directory.Exists(_filderPath))
        //            Directory.CreateDirectory(_filderPath);
        //       // if (formFiles == null || formFiles.Count == 0)
        //        //    return new JsonResult(new { status = "-1", message = "û��ѡ���κ��ļ�" });
        //        // IFormFile file = formFiles.Files[0];
        //        // string fileExtension = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);//��ȡ�ļ����ƺ�׺
        //        // if (fileExtension != ".jmx")
        //        //     return new JsonResult(new { status = -1, message = "�ļ����Ͳ��Ϸ�" });
        //        // Stream stream = file.OpenReadStream();
        //        // byte[] bytes = new byte[stream.Length];
        //        // stream.Read(bytes, 0, bytes.Length);
        //        // stream.Seek(0, SeekOrigin.Begin);
        //        // FileStream fs = new FileStream($"{_filderPath}/{guid}{fileExtension}", FileMode.Create);
        //        // BinaryWriter bw = new BinaryWriter(fs);
        //        // bw.Write(bytes);
        //        // bw.Close();
        //        // fs.Close();
        //        return new JsonResult(new { status = 1, id = guid, message = "�ϴ��ɹ�" });
        //    });

        //}   


        /// <summary>
        /// �ϴ��ļ�
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        [HttpPost("UploadFiles")]
        public async Task<IActionResult> PostUploadFiles(List<IFormCollection> files)
        {
            HttpRequest request = HttpContext.Request;
            var file = request.Form.Files;
            int fileCount= file.Count;
            var str = string.Empty;

           
       
            //MultipartFormDataStreamProvider

            //string args = Request.Form["files"];
            //long size = files.Sum(f => f.Length);

            //// ��ʱ�ļ���·��
            //var filePath = Path.GetTempFileName();

            //foreach (var formFile in files)
            //{
            //    //ȡ��׺��
            //    var fileN = formFile.FileName.ToString();
            //    var fileLastName = fileN.Substring(fileN.LastIndexOf(".") + 1,
            //        (fileN.Length - fileN.LastIndexOf(".") - 1));

            //    filePath = @"Upload\" + "one." + fileLastName;//�����ļ���·��
            //    if (formFile.Length > 0)
            //    {
            //        //����·�������ļ�
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await formFile.CopyToAsync(stream);
            //        }
            //    }
            //}
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return await Task<JsonResult>.Run(()=> {
                return Ok(new { count = files.Count});
            });
          
        }

    }
}
