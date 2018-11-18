using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;

namespace Smi.Api.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        [SuppressMessage("ReSharper", "UnusedMember.Global", Justification = "Called by the runtime")]
        public static void UseCorrelationIdMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}