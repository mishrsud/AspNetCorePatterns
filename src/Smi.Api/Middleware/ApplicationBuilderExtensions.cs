using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Smi.Api.Middleware
{
    [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by the runtime")]
    public static class ApplicationBuilderExtensions
    {
        public static void UseCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
        }
        
        public static void UseCustomExceptionHandlingMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<UnhandledExceptionHandlerMiddleware>();
        }
    }
}