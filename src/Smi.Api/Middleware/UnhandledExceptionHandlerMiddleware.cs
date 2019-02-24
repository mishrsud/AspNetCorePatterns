using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Smi.Api.Middleware
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by the runtime")]
    public class UnhandledExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnhandledExceptionHandlerMiddleware> _logger;
        private const string ApplicationJson = "application/json";

        public UnhandledExceptionHandlerMiddleware(
            RequestDelegate next,
            ILogger<UnhandledExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, nameof(UnhandledExceptionHandlerMiddleware));
                await HandleExceptionAsync(httpContext, exception.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, string exceptionMessage)
        {
            httpContext.Response.ContentType = ApplicationJson;
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            var problemDetails = new
            {
                ProblemDetails = new[]
                {
                    new
                    {
                        Reason = $"The server encountered and error while processing this request. Correlation id: {httpContext.Response.Headers["Request-Id"]}",
                        Detail = exceptionMessage
                    }
                }
            };
            
            return httpContext.Response.WriteAsync(JsonConvert.SerializeObject(problemDetails));
        }
    }
}