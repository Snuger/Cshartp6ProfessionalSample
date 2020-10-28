using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
namespace serilogSamples.Extensions
{
    public static class SerilogApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app, string messageTemplate) => app.UseSerilogRequestLogging(messageTemplate);

        public static IApplicationBuilder UseSerilogRequestLogging(this IApplicationBuilder app, Action<RequestLoggingOptions> options = null)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));
            var opts = app.ApplicationServices.GetService<IOptions<RequestLoggingOptions>>()?.Value ?? new RequestLoggingOptions();
            options.Invoke(opts);
            if (opts.MessageTemplate == null) throw new ArgumentException($"{nameof(opts.MessageTemplate)} cannot be null.");
            if (opts.GetLevel == null) throw new ArgumentException($"{nameof(opts.GetLevel)} cannot be null.");

            return app.UseMiddleware<RequestLoggingMiddleware>(opts);

        }

    }
}