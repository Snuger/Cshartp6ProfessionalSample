using System.IO;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;


namespace serilogSamples
{
    public class SerilogLoggingActionFilter : IActionFilter
    {
        private readonly IDiagnosticContext _diagnosticContext;
        public SerilogLoggingActionFilter(IDiagnosticContext diagnosticContext)
        {
            _diagnosticContext = diagnosticContext;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var reader = new StreamReader(context.HttpContext.Request.Body);
            var requestContent = reader.ReadToEndAsync();
            _diagnosticContext.Set("Content", requestContent.Result);
            _diagnosticContext.Set("RouteData", context.ActionDescriptor.RouteValues);
            _diagnosticContext.Set("ActionName", context.ActionDescriptor.DisplayName);
            _diagnosticContext.Set("ActionId", context.ActionDescriptor.Id);
            _diagnosticContext.Set("ValidationState", context.ModelState.IsValid);
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}