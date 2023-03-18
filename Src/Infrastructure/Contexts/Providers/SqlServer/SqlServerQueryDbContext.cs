using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pumpkin.Domain.Framework.Helpers;
using Pumpkin.Infrastructure.Framework.Data.Context;

namespace Pumpkin.Infrastructure.Contexts.Providers.SqlServer;

public sealed class SqlServerQueryDbContext : QueryDbContext
{
    public SqlServerQueryDbContext(DbContextOptions<SqlServerQueryDbContext> options, IHttpContextAccessor accessor) 
        : base(options, accessor)
    {
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(GlobalConfig.Config.GetConnectionString(ConnectionStringName), builder => builder.CommandTimeout(30));
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(options);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelCreatingCommonConfig(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}