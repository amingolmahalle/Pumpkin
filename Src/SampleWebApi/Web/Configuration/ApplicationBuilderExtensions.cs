using Pumpkin.Common.Helpers;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace SampleWebApi.Web.Configuration;

public static class ApplicationBuilderExtensions
{
    public static void UseSwaggerAndUi(this IApplicationBuilder app)
    {
        Helpers.NotNull(app, nameof(app));

        //Swagger middleware for generate "Open API Documentation" in swagger.json
        app.UseSwagger(options =>
        {
            //options.RouteTemplate = "api-docs/{documentName}/swagger.json";
        });

        //Swagger middleware for generate UI from swagger.json
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");
            options.SwaggerEndpoint("/swagger/v2/swagger.json", "V2 Docs");
            options.DocExpansion(DocExpansion.None);

        });

        //ReDoc UI middleware. ReDoc UI is an alternative to swagger-ui
        app.UseReDoc(options =>
        {
            options.SpecUrl("/swagger/v1/swagger.json");
            //options.SpecUrl("/swagger/v2/swagger.json");
        });
    }
}