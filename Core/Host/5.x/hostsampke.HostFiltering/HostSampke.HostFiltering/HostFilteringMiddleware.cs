using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace HostSampke.HostFiltering
{
    public class HostFilteringMiddleware
    {
        private static readonly byte[] DefaultResponse = Encoding.ASCII.GetBytes(
          "<!DOCTYPE HTML PUBLIC \"-//W3C//DTD HTML 4.01//EN\"\"http://www.w3.org/TR/html4/strict.dtd\">\r\n"
          + "<HTML><HEAD><TITLE>Bad Request</TITLE>\r\n"
          + "<META HTTP-EQUIV=\"Content-Type\" Content=\"text/html; charset=us-ascii\"></ HEAD >\r\n"
          + "<BODY><h2>Bad Request - Invalid Hostname</h2>\r\n"
          + "<hr><p>HTTP Error 400. The request hostname is invalid.</p>\r\n"
          + "</BODY></HTML>");

        private readonly RequestDelegate _next;
        private ILogger<HostFilteringMiddleware> _logger;
        private readonly IOptionsMonitor<HostFilteringOptions> _optionsMonitor;
        private HostFilteringOptions _options;
        private IList<StringSegment> _allowedHosts;
        private bool? _allowAnyNonEmptyHost;

        public HostFilteringMiddleware(RequestDelegate next, ILogger<HostFilteringMiddleware> logger, IOptionsMonitor<HostFilteringOptions> optionsMonitor, IList<StringSegment> allowedHosts, bool? allowAnyNonEmptyHost)
        {
            _next = next;
            _logger = logger;
            _optionsMonitor = optionsMonitor ?? throw new ArgumentNullException(nameof(optionsMonitor));
            _options = optionsMonitor.CurrentValue;
            _allowedHosts = allowedHosts;
            _allowAnyNonEmptyHost = allowAnyNonEmptyHost;
            _optionsMonitor.OnChange(options =>
            {
                _options = options;
                _allowedHosts = new List<StringSegment>();
                _allowAnyNonEmptyHost = null;
            });
        }


        public Task Invoke(HttpContext context)
        {
            var allowedHosts = EnsureConfigured();
            if (!CheckHost(context, allowedHosts))
            {
                return HostValidationFailed(context);
            }
            return _next(context);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private Task HostValidationFailed(HttpContext context)
        {
            context.Response.StatusCode = 400;
            if (_options.IncludeFailureMessage)
            {
                context.Response.ContentLength = DefaultResponse.Length;
                context.Response.ContentType = "text/html";
                return context.Response.Body.WriteAsync(DefaultResponse, 0, DefaultResponse.Length);
            }
            return Task.CompletedTask;
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IList<StringSegment> EnsureConfigured()
        {
            if (_allowAnyNonEmptyHost == true || _allowedHosts?.Count > 0)
            {
                return _allowedHosts;
            }
            return Configure();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private IList<StringSegment> Configure()
        {
            var allowedHosts = new List<StringSegment>();
            if (_options.AllowedHosts?.Count > 0 && !TryProcessHosts(_options.AllowedHosts, allowedHosts))
            {
                _logger.WildcardDetected();
                _allowedHosts = allowedHosts;
                _allowAnyNonEmptyHost = true;
                return _allowedHosts;
            }
            if (allowedHosts.Count == 0)
            {
                throw new InvalidOperationException("No allowed hosts were configured.");
            }
            if (_logger.IsEnabled(LogLevel.Debug))
            {
                _logger.AllowedHosts(string.Join("; ", allowedHosts));
            }
            _allowedHosts = allowedHosts;
            return _allowedHosts;
        }

        private bool TryProcessHosts(IEnumerable<string> incoming, IList<StringSegment> results)
        {
            foreach (var entity in incoming)
            {
                var host = new HostString(entity).ToUriComponent();
                if (IsTopLevelWildcard(host))
                {
                    return false;
                }
                if (!results.Any(c => c == host))
                {
                    results.Add(host);
                }
            }
            return true;
        }

        private bool IsTopLevelWildcard(string host)
        {
            return (string.Equals("*", host, StringComparison.Ordinal)
        || string.Equals("[::]", host, StringComparison.Ordinal)
        || string.Equals("0.0.0.0", host, StringComparison.Ordinal));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool CheckHost(HttpContext context, IList<StringSegment> allowedHosts)
        {
            var hosts = context.Request.Headers[HeaderNames.Host].ToString();
            if (hosts.Length == 0)
            {
                return IsEmptyHostAllowed(context);
            }
            if (_allowAnyNonEmptyHost == true)
            {
                _logger.AllHostsAllowed();
                return true;
            }

            return CheckHostInAllowList(allowedHosts, hosts);
        }


        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool CheckHostInAllowList(IList<StringSegment> allowedHosts, string host)
        {
            if (HostString.MatchesAny(new StringSegment(host), allowedHosts))
            {
                _logger.AllowedHostMatched(host);
                return true;
            }

            _logger.NoAllowedHostMatched(host);
            return false;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private bool IsEmptyHostAllowed(HttpContext context)
        {
            if (!_options.AllowEmptyHost)
            {
                //记录日志
                _logger.RequestRejectedMissingHost(context.Request.Protocol);
                return false;

            }
            _logger.RequestAllowedMissingHost(context.Request.Protocol);
            return true;
        }
    }
}