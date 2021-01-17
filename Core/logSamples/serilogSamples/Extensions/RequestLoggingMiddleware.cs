using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Serilog;
using Serilog.Events;
using Serilog.Extensions.Hosting;
using Serilog.Parsing;

namespace serilogSamples.Extensions
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        readonly DiagnosticContext _diagnosticContext;

        readonly Action<DiagnosticContext, HttpContext> _enrichDiagnosticContext;

        readonly MessageTemplate _messageTemplate;

        readonly ILogger _logger;

        readonly Func<HttpContext, double, Exception, LogEventLevel> _getLevel;

        static readonly LogEventProperty[] NoProperties = new LogEventProperty[0];


        public RequestLoggingMiddleware(RequestDelegate next, DiagnosticContext diagnosticContext, RequestLoggingOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _diagnosticContext = diagnosticContext ?? throw new ArgumentNullException(nameof(diagnosticContext));

            _getLevel = options.GetLevel;
            _enrichDiagnosticContext = options.EnrichDiagnosticContext;
            _messageTemplate = new MessageTemplateParser().Parse(options.MessageTemplate);
            _logger = options.Logger.ForContext<RequestLoggingMiddleware>();

        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var start = Stopwatch.GetTimestamp();

            var collectior = _diagnosticContext.BeginCollection();
            try
            {
                await _next(context);
                var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                var statusCode = context.Response.StatusCode;
                LogCompletion(context, collectior, statusCode, elapsedMs, null);
            }
            catch (System.Exception ex)
             when (LogCompletion(context, collectior, 500, GetElapsedMilliseconds(start, Stopwatch.GetTimestamp()), ex))
            { }
            finally
            {
                collectior.Dispose();
            }
        }


        static double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }

        bool LogCompletion(HttpContext context, DiagnosticContextCollector collector, int statusCode, double elapsedMs, Exception ex)
        {
            var logger = _logger ?? Log.ForContext<RequestLoggingMiddleware>();
            var leval = _getLevel(context, elapsedMs, ex);
            if (!logger.IsEnabled(leval)) return false;
            _enrichDiagnosticContext?.Invoke(_diagnosticContext, context);
            if (!collector.TryComplete(out var collectedProperties))
                collectedProperties = NoProperties;

            var properties = collectedProperties.Concat(
                new[]{
                    new LogEventProperty("RequestMethod",new ScalarValue(context.Request.Method)),
                    new LogEventProperty("RequestPath",new ScalarValue(GetPath(context))),
                    new LogEventProperty("StatusCode",new ScalarValue(statusCode)),
                    new LogEventProperty("Elapsed",new ScalarValue(elapsedMs))
                }
            );
            var evt = new LogEvent(DateTimeOffset.Now, leval, ex, _messageTemplate, properties);
            logger.Write(evt);
            return false;

        }


        static string GetPath(HttpContext httpContext)
        {
            /*
                In some cases, like when running integration tests with WebApplicationFactory<T>
                the RawTarget returns an empty string instead of null, in that case we can't use
                ?? as fallback.
            */
            var requestPath = httpContext.Features.Get<IHttpRequestFeature>()?.RawTarget;
            if (string.IsNullOrEmpty(requestPath))
            {
                requestPath = httpContext.Request.Path.ToString();
            }

            return requestPath;
        }

    }
}