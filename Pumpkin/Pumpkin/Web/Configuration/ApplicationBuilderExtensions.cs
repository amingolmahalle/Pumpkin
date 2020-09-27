using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pumpkin.Common.Helpers;
using Pumpkin.Contract.Domain;
using Pumpkin.Data;

namespace Pumpkin.Web.Configuration
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHsts(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            Helpers.NotNull(app, nameof(app));
            Helpers.NotNull(env, nameof(env));

            if (!env.IsDevelopment())
                app.UseHsts();
        }

        public static void InitializeDatabase<TDbContext>(this IApplicationBuilder app)
            where TDbContext : DatabaseContext
        {
            //Use C# 8 using variables
            using var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var dbContext = scope.ServiceProvider.GetService<TDbContext>(); //Service locator

            //Dos not use Migrations, just Create Database with latest changes
            //dbContext.Database.EnsureCreated();
            
            //Applies any pending migrations for the context to the database like (Update-Database)
            dbContext.Database.Migrate();

            var dataInitializers = scope.ServiceProvider.GetServices<IDataInitializer>();

            foreach (var dataInitializer in dataInitializers)
                dataInitializer.SeedData();
        }

        public static void UseCustomCors(this IApplicationBuilder app)
        {
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
    }
}