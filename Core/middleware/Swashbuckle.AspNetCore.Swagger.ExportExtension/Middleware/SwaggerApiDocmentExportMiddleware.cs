using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger.ExportExtension.SwaggerDocGen;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


#if NETSTANDARD2_0 
using IWebHostEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
#endif

namespace Swashbuckle.AspNetCore.Swagger.ExportExtension.Middleware
{
    public class SwaggerApiDocmentExportMiddleware
    { 
        private readonly StaticFileMiddleware _staticFileMiddleware; 

        public SwaggerApiDocmentExportMiddleware(RequestDelegate next, IWebHostEnvironment environment, ILoggerFactory loggerFactory)
        {                 
            StaticFileOptions options = new StaticFileOptions()
            {
                RequestPath = "/doc",
                FileProvider = new EmbeddedFileProvider(typeof(SwaggerApiDocmentExportMiddleware).Assembly, $"{typeof(SwaggerApiDocmentExportMiddleware).Assembly.ManifestModule.Name.Replace(".dll", ".node_modules")}")
            };
            _staticFileMiddleware = new StaticFileMiddleware(next, environment, Options.Create(options), loggerFactory);
       
        }

        public async Task InvokeAsync(HttpContext context)
        {          
            if (RequestingSwaggerExport(context.Request))
                await ExportApiDocument(context);
            await _staticFileMiddleware.Invoke(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool RequestingSwaggerExport(HttpRequest request)
        {
            var httpMethod = request.Method;
            var path = request.Path.Value;
            if (httpMethod == "GET" && Regex.IsMatch(path, $"^/?/doc/ExportApiDocument/?$", RegexOptions.IgnoreCase)) return true;
            return false;
        }

        private async Task ExportApiDocument(HttpContext context)
        {
            var version =context.Request.Query["version"];
            var service = (ISwaggerDocGenerator) context.RequestServices.GetService(typeof(ISwaggerDocGenerator));
            var stream = await service.GetSwaggerDocStreamAsync(version);    
            var tmpPath = Path.Combine(AppContext.BaseDirectory, "tmp");
            if (!Directory.Exists(tmpPath))
                Directory.CreateDirectory(tmpPath);
            var makeDownFilePath = Path.Combine(tmpPath, $"{Guid.NewGuid().ToString()}.md");
            byte[] byteArray = stream.ToArray();
            using (FileStream fs = File.Create(makeDownFilePath))
                await fs.WriteAsync(byteArray, 0, byteArray.Length);
            var downloadFile = new FileInfo(makeDownFilePath);
            context.Response.ContentType = "application/octet-stream";
            context.Response.Headers.Add("Content-Length", downloadFile.Length.ToString());
            context.Response.Headers.Add("Content-Disposition", "attachment; filename=*.md");
            context.Response.Headers.Add("Content-Transfer-Encoding", "binary");
            await context.Response.SendFileAsync(new PhysicalFileInfo(new FileInfo(makeDownFilePath)), 0, downloadFile.Length);
        }

    }

    
}
