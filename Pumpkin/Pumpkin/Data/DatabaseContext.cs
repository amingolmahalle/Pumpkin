using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pumpkin.Core;
using Pumpkin.Data.Listeners;
using Pumpkin.Utils;

namespace Pumpkin.Data
{
    public abstract class DatabaseContext : DbContext
    {
        protected DatabaseContext(DbContextOptions options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.NeedToMappingConfig();

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
            var insertService = new ServiceCollection()
                .AddTransient<IBeforeInsertListener, HistoryBeforeInsert>();
            
            var insertServiceProvider = insertService.BuildServiceProvider();
            
            var insertListeners = insertServiceProvider.GetServices<HistoryBeforeInsert>().ToArray();
            
            if (insertListeners.Any())
            {
                var added = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added);

                foreach (var entityEntry in added)
                {
                    foreach (var beforeInsertListener in insertListeners)
                    {
                        var changedEntity = entityEntry.Map();

                        beforeInsertListener.OnBeforeInsert(changedEntity);
                    }
                }
            }
            
            var updateService = new ServiceCollection()
                .AddTransient<IBeforeUpdateListener, HistoryBeforeUpdate>();
                
            var updateServiceProvider = updateService.BuildServiceProvider();
                
            var modifyListeners = updateServiceProvider.GetServices<HistoryBeforeUpdate>().ToArray();
            
            if (modifyListeners.Any())
            {
                var modified = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Modified);

                foreach (var entityEntry in modified)
                {
                    foreach (var beforeUpdateListener in modifyListeners)
                    {
                        var changedEntity = entityEntry.Map();

                        beforeUpdateListener.OnBeforeUpdate(changedEntity);
                    }
                }
            }
        }
    }
}