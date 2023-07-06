using System.Reflection;
using ClientWebApi.Middlewares;
using Framework.Exceptions;
using Pumpkin.Domain.Framework.Events;
using Pumpkin.Domain.Framework.Exceptions;
using Pumpkin.Domain.Framework.Helpers;
using Pumpkin.Infrastructure.Framework.Documentation.Swagger;
using Pumpkin.Infrastructure.Framework.Extensions;

namespace ClientWebApi;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        GlobalConfig.Config = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // NLogConfigurationManager.Configure("SpaceTravel");
        // LogManager.Use<NLogFactory>();
        
        services.AddCustomControllers("Pumpkin");

        services.AddCors(options => options.AddPolicy("SpaceTravelCorsPolicy", builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()));

        services.AddCustomApiVersioning();
        services.AddCustomSwagger();

        // services.AddScoped<ICacheService, RedisService>();
        // services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();
        // services.AddSingleton<BusHandler, RabbitMqBusHandler>();

        services.DynamicInject(Configuration, "Pumpkin");

        if (Program.SetupConsumers)
            SetupConsumers(services);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        
        app.UseCustomErrorHandler();

        app.UseCustomSwagger();

        if (env.IsProduction())
            app.UseHttpsRedirection();

        app.UseStaticFiles();

        app.UseCustomLocalization();

        app.UseRouting();

        app.UseCustomCors();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }

    public void SetupConsumers(IServiceCollection services)
    {
        var serviceProvider = services?.BuildServiceProvider();
        
        if (serviceProvider == null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable), new List<KeyValuePair<string, string>>
            {
                new(":پیام:", "امکان دریافت لیست سرویس‌ها وجود ندارد.")
            });


        var service = serviceProvider.GetService<BusHandler>();
        
        if (service is null)
            throw new Dexception(Situation.Make(SitKeys.Unprocessable), new List<KeyValuePair<string, string>>
            {
                new(":پیام:", "امکان برقرار ارتباط با صف وجود ندارد.")
            });

        service.ExtractConsumers(serviceProvider, "Pumpkin");
        service.StartListening();
    }
}