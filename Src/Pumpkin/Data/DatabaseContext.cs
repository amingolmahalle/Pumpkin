using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Common.Extensions;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;

namespace Pumpkin.Data;

public abstract class DatabaseContext : DbContext
{
    private readonly IServiceProvider _serviceProvider;

    protected DatabaseContext(
        DbContextOptions options,
        IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider = serviceProvider;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.NeedToRegisterEntitiesConfig<IEntity>();
        modelBuilder.NeedToRegisterMappingConfig();
        modelBuilder.AddSequentialGuidForIdConvention();
        modelBuilder.AddPluralizingTableNameConvention();
    }

    public override int SaveChanges()
    {
        var entityEntries = ChangeTracker.Entries().ToList();

        _cleanString(entityEntries);

        ChangeTracker.DetectChanges();

        RegisterBeforeListener(entityEntries);

        ChangeTracker.AutoDetectChangesEnabled = false;

        int result = base.SaveChanges();

        ChangeTracker.AutoDetectChangesEnabled = true;

        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entityEntries = ChangeTracker.Entries().ToList();

        _cleanString(entityEntries);

        ChangeTracker.DetectChanges();

        RegisterBeforeListener(entityEntries);

        ChangeTracker.AutoDetectChangesEnabled = false;

        int result = await base.SaveChangesAsync(cancellationToken);

        ChangeTracker.AutoDetectChangesEnabled = true;

        return result;
    }

    private void _cleanString(IEnumerable<EntityEntry> entityEntries)
    {
        var changedEntities = entityEntries
            .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

        foreach (var item in changedEntities)
        {
            if (item.Entity == null)
                continue;

            var properties = item.Entity
                .GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite && p.PropertyType == typeof(string));

            foreach (var property in properties)
            {
                var val = (string) property.GetValue(item.Entity, null);

                if (val.HasItem())
                {
                    var newVal = val.FixPersianChars().Fa2En().NullIfEmpty();

                    if (newVal.Equals(val))
                        continue;

                    property.SetValue(item.Entity, newVal, null);
                }
            }
        }
    }

    private void RegisterBeforeListener(IEnumerable<EntityEntry> entityEntries)
    {
        var beforeInsertListener = _serviceProvider.GetService<IBeforeInsertListener>();
        var beforeUpdateListener = _serviceProvider.GetService<IBeforeUpdateListener>();
        var beforeDeleteListener = _serviceProvider.GetService<IBeforeDeleteListener>();

        var entries = entityEntries
            .Where(e => e.Entity
                .GetType()
                .IsAssignableFromGeneric(typeof(IEntity)))
            .ToList();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    beforeInsertListener?.OnBeforeInsert(entry);
                    break;
                case EntityState.Modified:
                    beforeUpdateListener?.OnBeforeUpdate(entry);
                    break;
                case EntityState.Deleted:
                    beforeDeleteListener?.OnBeforeDelete(entry);
                    break;
            }
        }
    }
}