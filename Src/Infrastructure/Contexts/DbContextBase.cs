using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pumpkin.Domain.Framework.Entities;
using Pumpkin.Domain.Framework.Helpers;
using Pumpkin.Infrastructure.Framework.Extensions;

namespace Pumpkin.Infrastructure.Contexts;

public class DbContextBase : DbContext
{
    private readonly AuditingInterceptor _auditingInterceptor;
    protected string ConnectionStringName = string.Empty;

    public DbContextBase(DbContextOptions options, IHttpContextAccessor accessor)
        : base(options)
    {
        _auditingInterceptor = new AuditingInterceptor(accessor);
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var logToConsole = !string.IsNullOrEmpty(GlobalConfig.Config["Logging:Context:Console"]) && bool.Parse(GlobalConfig.Config["Logging:Context:Console"]);
        var logToDebug = !string.IsNullOrEmpty(GlobalConfig.Config["Logging:Context:Debug"]) && bool.Parse(GlobalConfig.Config["Logging:Context:Debug"]);

        options.AddInterceptors(_auditingInterceptor);

        if (logToConsole)
            options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));

        if (logToDebug)
            options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddDebug(); }));

        if (logToConsole || logToDebug)
            options.EnableSensitiveDataLogging().EnableDetailedErrors();

        options.UseSnakeCaseNamingConvention();
    }

    protected void ModelCreatingCommonConfig(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.NeedToRegisterEntitiesConfig<IEntity>();
        modelBuilder.NeedToRegisterMappingConfig();

        base.OnModelCreating(modelBuilder);
    }

    #endregion
}