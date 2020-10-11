using System;
using Microsoft.AspNetCore.Builder;

namespace HostSampke.HostFiltering
{
    public static class HostFilterBuilderExtensions
    {
        public static IApplicationBuilder UseHostFiltering(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            app.UseMiddleware<HostFilteringMiddleware>();

            return app;

        }
    }
}