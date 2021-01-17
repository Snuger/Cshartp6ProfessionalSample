using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;


namespace serilogSamples
{
    public class SerilogLoggingActionFilter : ActionFilterAttribute
    {
        private readonly IDiagnosticContext _diagnosticContext;
        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            if (context.HttpContext.Request.Method.ToUpper() == "POST" || context.HttpContext.Request.Method.ToUpper() == "PUT")
            {
                //var reader = new StreamReader(context.HttpContext.Request.Body);
                //var requestContent = reader.ReadToEndAsync();
                var request = context.HttpContext.Request;
                //启动倒带方式
                //request.EnableBuffering();
                request.Body.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(request.Body, Encoding.UTF8))
                {
                    var param = await reader.ReadToEndAsync();
                    _diagnosticContext.Set("Content", param);
                }
                //request.Body.Seek(0, SeekOrigin.Begin);
            }
            _diagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
            _diagnosticContext.Set("ValidationState", context.ModelState.IsValid);
           await base.OnActionExecutionAsync(context, next);
        }

        public  void OnActionExecuting(ActionExecutingContext context)
        {
           
        }

        public void OnActionExecuted(ActionExecutedContext context) { 
        
        }
    }
}