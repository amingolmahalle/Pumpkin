using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Framework.Extensions;

public static class ApplicationBuilderExtensions
{
   public static void UseCustomLocalization(this IApplicationBuilder app)
    {
        var localizeOption = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(localizeOption?.Value);
    }
        
    public static void UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("YourProjectNameCorsPolicy");
    }
}