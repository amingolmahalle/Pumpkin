using System.Globalization;
using Infrastructure.Framework.HttpRouting;
using Infrastructure.Framework.Tools.MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Framework.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCustomLocalization(this IServiceCollection services)
    {
        services.Configure<RequestLocalizationOptions>(options =>
        {
            var cultures = new List<CultureInfo>
            {
                new("en-US"),
                new("fa-IR")
            };

            options.RequestCultureProviders = new List<IRequestCultureProvider>
            {
                new QueryStringRequestCultureProvider(),
                new CookieRequestCultureProvider()
            };

            options.DefaultRequestCulture = new RequestCulture("fa-IR", "fa-IR");
            options.SupportedCultures = cultures;
            options.SupportedUICultures = cultures;

            var cultureInfo = new CultureInfo("fa-IR") {NumberFormat = {CurrencySymbol = "IRT"}};
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        });
    }

    public static void AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(v =>
        {
            v.ReportApiVersions = true;
            v.AssumeDefaultVersionWhenUnspecified = true;
            v.DefaultApiVersion = new ApiVersion(1, 0);
        });
        services.AddVersionedApiExplorer(o =>
        {
            o.GroupNameFormat = "'v'VVV";
            o.SubstituteApiVersionInUrl = true;
        });
    }

    public static void AddCustomControllers(this IServiceCollection services, string nameSpace)
    {
        services
            .AddHttpContextAccessor()
            .AddMediatR(nameSpace)
            .AddControllers(options => { options.Conventions.Add(new RouteTokenTransformerConvention(new SnakeCaseRouter())); })
            .AddNewtonsoftJson(options => { options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; })
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
    }

    public static void AddAnyCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(options => options
            .AddPolicy("SpaceTravelCorsPolicy", builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));
    }
}