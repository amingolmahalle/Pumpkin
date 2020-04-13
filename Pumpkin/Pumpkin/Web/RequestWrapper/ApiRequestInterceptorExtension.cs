using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Pumpkin.Web.RequestWrapper
{
    public static class ApiRequestInterceptorExtension
    {
        public static IApplicationBuilder UseRequestInterceptor(this IApplicationBuilder builder,
            List<string> pathExceptions)
        {
            Exceptions = pathExceptions;

            return builder.UseMiddleware<ApiRequestInterceptor>();
        }

        internal static List<string> Exceptions { get; set; }
    }
}