using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;

namespace Pumpkin.Web.RequestWrapper
{
    public static class ApiRequestInterceptorExtension
    {
        public static void UseRequestInterceptor(this IApplicationBuilder builder,
            List<string> pathExceptions)
        {
            Exceptions = pathExceptions;

            builder.UseMiddleware<ApiRequestInterceptor>();
        }

        internal static List<string> Exceptions { get; set; }
    }
}