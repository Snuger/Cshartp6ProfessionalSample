using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netCoreSample.Middleware
{
    public class HeaderMiddlware
    {
        private readonly RequestDelegate _next;

        public HeaderMiddlware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext) {
            httpContext.Response.Headers.Add("sampleheader",
                new string [] { "addheadermiddleware" });
            return _next(httpContext);
        }

    }


    public static class HeaderMiddlewareExtensions
    {
        public static IApplicationBuilder UserHeaderMiddleware(
            this IApplicationBuilder builder)=>
            builder.UseMiddleware<HeaderMiddlware>();
    }
}
