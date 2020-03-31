using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pumpkin.Core;
using Pumpkin.Utils;

namespace Pumpkin.Data
{
    public abstract class DatabaseContext : DbContext
    {
        protected DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.NeedToMappingConfigExtension();

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            _cleanString();
            
            ChangeTracker.DetectChanges();
            
            ChangeTracker.AutoDetectChangesEnabled = false;
            
            int result = base.SaveChanges();
            
            ChangeTracker.AutoDetectChangesEnabled = true;
            
            return result;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _cleanString();
            
            ChangeTracker.DetectChanges();
            
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

        // private void _setAuditEntityProperties()
        // {
        //     //--Get User Id If Authenticated.
        //     int? userId = _getUserId();
        //
        //     var entityEntries = ChangeTracker.Entries<BaseEntity>().Where(x => x.Entity.GetType()
        //                                      .IsAssignableFrom(typeof(BaseEntity))
        //                                       && (x.State == EntityState.Added
        //                                        || x.State == EntityState.Modified
        //                                        || x.State == EntityState.Deleted));
        //
        //     foreach (var entry in entityEntries)
        //     {
        //         if (entry.State == EntityState.Added)
        //         {
        //             entry.Entity.CreatedUserId = userId;
        //             entry.Entity.CreatedDate = DateTimeOffset.Now;
        //         }
        //         if (entry.State == EntityState.Modified)
        //         {
        //             entry.Entity.ModifiedUserId = userId;
        //             entry.Entity.ModifiedDate = DateTimeOffset.Now;
        //         }
        //     }
        // }
    }
}