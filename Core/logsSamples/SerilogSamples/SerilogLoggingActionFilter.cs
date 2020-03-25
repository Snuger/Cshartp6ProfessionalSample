using System;
using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace SerilogSamples
{

    public class SerilogLoggingActionFilter :IActionFilter
    {

        private readonly IDiagnosticContext _diagnosticContext;

        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
             _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
              var reader=new StreamReader(context.HttpContext.Request.Body);
              var readContent=reader.ReadToEndAsync();
              _diagnosticContext.Set("Content",readContent.Result);
              
            _diagnosticContext.Set("RouteData",context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("ActionName",context.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("ActionId",context.ActionDescriptor.Id);


        }
    }

}