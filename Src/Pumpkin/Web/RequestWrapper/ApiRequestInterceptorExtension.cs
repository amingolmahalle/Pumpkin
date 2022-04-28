using Microsoft.AspNetCore.Builder;

namespace Pumpkin.Web.RequestWrapper;

public static class ApiRequestInterceptorExtension
{
    public static void UseRequestInterceptor(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ApiRequestInterceptor>();
    }
}