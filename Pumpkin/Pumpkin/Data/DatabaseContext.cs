using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Contract.Domain;
using Pumpkin.Contract.Listeners;
using Pumpkin.Core.Registration;
using Pumpkin.Utils.Extensions;

namespace Pumpkin.Data
{
    public abstract class DatabaseContext : DbContext
    {
        private readonly IBeforeInsertListener _beforeInsertListener;

        private readonly IBeforeUpdateListener _beforeUpdateListener;

        private readonly IBeforeDeleteListener _beforeDeleteListener;

        protected DatabaseContext(
            DbContextOptions options,
            IBeforeInsertListener beforeInsertListener,
            IBeforeUpdateListener beforeUpdateListener,
            IBeforeDeleteListener beforeDeleteListener) : base(options)
        {
            _beforeInsertListener = beforeInsertListener;
            _beforeUpdateListener = beforeUpdateListener;
            _beforeDeleteListener = beforeDeleteListener;
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
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity
                    .GetType()
                    .IsAssignableFromGeneric(typeof(IEntity<>)))
                .ToList();

            foreach (var entry in entries)
            {
                var entity = entry.Entity;


                switch (entry.State)
                {
                    case EntityState.Added:
                        _beforeInsertListener.OnBeforeInsert(entity);
                        break;
                    case EntityState.Modified:
                        _beforeUpdateListener.OnBeforeUpdate(entity);
                        break;
                    case EntityState.Deleted:
                        _beforeDeleteListener.OnBeforeDelete(entity);
                        break;
                }
            }
        }
    }
}