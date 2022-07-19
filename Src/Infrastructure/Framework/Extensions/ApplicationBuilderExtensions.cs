using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Infrastructure.Framework.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomSwagger(this IApplicationBuilder app)
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.DocExpansion(DocExpansion.None);
            c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "v1.0");
        });
    }

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