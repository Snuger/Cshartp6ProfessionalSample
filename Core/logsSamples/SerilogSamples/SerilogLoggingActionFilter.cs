using System;
using System.Web;
using Microsoft.AspNetCore.Http;
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

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _diagnosticContext.Set("ActionName", filterContext.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("Content", filterContext.ActionDescriptor.RouteValues);

        }
    }

}