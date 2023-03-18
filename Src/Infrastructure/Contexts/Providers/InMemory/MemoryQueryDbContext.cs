using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Infrastructure.Framework.Data.Context;

namespace Pumpkin.Infrastructure.Contexts.Providers.InMemory;

public sealed class MemoryQueryDbContext : QueryDbContext
{
    public MemoryQueryDbContext(DbContextOptions<MemoryQueryDbContext> options, IHttpContextAccessor accessor)
        : base(options, accessor)
    {
    }

    #region << Model Creating >>

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseInMemoryDatabase("FakeDatabase");
        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        base.OnConfiguring(options);

        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ModelCreatingCommonConfig(modelBuilder);
        base.OnModelCreating(modelBuilder);
    }

    #endregion
}