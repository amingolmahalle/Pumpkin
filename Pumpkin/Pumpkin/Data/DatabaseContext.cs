using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Core.Registration;
using Pumpkin.Utils.Extensions;

namespace Pumpkin.Data
{
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
            modelBuilder.NeedToRegisterAllEntitiesConfig();
            modelBuilder.NeedToRegisterMappingConfig();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            _cleanString();

            ChangeTracker.DetectChanges();

            RegisterBeforeListener();

            ChangeTracker.AutoDetectChangesEnabled = false;

            int result = base.SaveChanges();

            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();

            ChangeTracker.DetectChanges();

            RegisterBeforeListener();

            ChangeTracker.AutoDetectChangesEnabled = false;

            int result = await base.SaveChangesAsync(cancellationToken);

            ChangeTracker.AutoDetectChangesEnabled = true;

            return result;
        }

        private void _cleanString()
        {
            var changedEntities = ChangeTracker.Entries()
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

                        if (newVal == val)
                            continue;

                        property.SetValue(item.Entity, newVal, null);
                    }
                }
            }
        }

        private void RegisterBeforeListener()
        {
            var beforeInsertListener = _serviceProvider.GetService<IBeforeInsertListener>();
            var beforeUpdateListener = _serviceProvider.GetService<IBeforeUpdateListener>();
            var beforeDeleteListener = _serviceProvider.GetService<IBeforeDeleteListener>();
            
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity
                    .GetType()
                    .IsAssignableFromGeneric(typeof(IEntity<>)))
                .ToList();

            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        beforeInsertListener.OnBeforeInsert(entry);
                        break;
                    case EntityState.Modified:
                        beforeUpdateListener.OnBeforeUpdate(entry);
                        break;
                    case EntityState.Deleted:
                        beforeDeleteListener.OnBeforeDelete(entry);
                        break;
                }
            }
        }
    }
}