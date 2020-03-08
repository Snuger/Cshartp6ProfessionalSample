using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WabapiSamples.Units;


namespace WabapiSamples
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
         private readonly ILog _logger;

        public GlobalExceptionFilter(ILog logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {           
           _logger.Error(context.Exception.GetType().FullName.ToString(), context.Exception.ToString(),context.Exception.TargetSite.ToString());          
            context.Result = new JsonResult(context.Exception.Message);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }
}