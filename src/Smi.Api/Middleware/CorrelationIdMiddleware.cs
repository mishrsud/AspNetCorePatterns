using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Smi.Api.Middleware
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by the runtime")]
    [SuppressMessage("ReSharper", "ClassNeverInstantiated.Global", Justification = "Called by the runtime")]
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CorrelationIdMiddleware> _logger;

        public CorrelationIdMiddleware(
            RequestDelegate next,
            ILogger<CorrelationIdMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <remarks>
        /// This method sets the CorrelationId in the logged object to TraceIdentifier
        /// NOTE: If the request has a header called Request-Id, log lines out of this scope
        /// set the correlationId as the value of that header
        /// </remarks>
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            using (_logger.BeginScope("{CorrelationId}", httpContext.TraceIdentifier))
            {
                httpContext.Response.Headers["Request-Id"] = httpContext.TraceIdentifier;
                await _next(httpContext);
            }
        }
    }
}