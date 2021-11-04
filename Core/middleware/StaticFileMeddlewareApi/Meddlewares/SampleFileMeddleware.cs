using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StaticFileMeddlewareApi.Meddlewares
{
    public class SampleFileMeddleware
    {
        private readonly RequestDelegate _next;

        private readonly StaticFileMiddleware _staticFileMiddleware;    

        public SampleFileMeddleware(RequestDelegate next, IWebHostEnvironment hostingEnv, ILoggerFactory loggerFactory)
        {
            _next = next;

            var assembly = typeof(SampleFileMeddleware).GetTypeInfo().Assembly;
            var resources=assembly.GetManifestResourceNames();

            string current = string.Empty;
            resources.ToList().ForEach(x => {
                current = x;
            });


            var _fileProvider = new EmbeddedFileProvider(typeof(SampleFileMeddleware).GetTypeInfo().Assembly, "StaticFileMeddlewareApi.ad");
            var staticFileOptions = new StaticFileOptions
            {
                RequestPath = "/ads",
                FileProvider = _fileProvider              
            };
            _staticFileMiddleware = new StaticFileMiddleware(next, hostingEnv, Options.Create(staticFileOptions), loggerFactory);
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
            context.Response.StatusCode = StatusCodes.Status200OK;
            context.Response.ContentType = "application/json;charset=utf-8";
            await context.Response.WriteAsync("导出api");

        }
    }
}
