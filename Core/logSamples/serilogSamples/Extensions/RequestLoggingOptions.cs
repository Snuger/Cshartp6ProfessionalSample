using System;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace serilogSamples.Extensions
{
    public class RequestLoggingOptions
    {

        static LogEventLevel DefaultGetLevel(HttpContext ctx, double _, Exception ex) =>
                    ex != null ? LogEventLevel.Error :
                    ctx.Response.StatusCode > 499 ? LogEventLevel.Error : LogEventLevel.Information;


        const string DefaultRequestCompletionMessageTemplate = "HTTP {RequestMethod} {RequestPath} responded {StatusCode} in {Elapsed:0.0000} ms";


        public string MessageTemplate { get; set; }


        public ILogger Logger { get; set; }


        public Func<HttpContext, double, Exception, LogEventLevel> GetLevel { get; set; }

        public Action<IDiagnosticContext, HttpContext> EnrichDiagnosticContext;

        public RequestLoggingOptions()
        {
            GetLevel = DefaultGetLevel;
            MessageTemplate = DefaultRequestCompletionMessageTemplate;
        }
    }
}