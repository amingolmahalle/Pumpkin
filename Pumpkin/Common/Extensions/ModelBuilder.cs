using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pluralize.NET;

namespace Pumpkin.Common.Extensions
{
    public static partial class Extensions
    {
        public static void NeedToRegisterMappingConfig(this ModelBuilder modelBuilder)
        {
            var typesToRegister = AssemblyScanner.AllTypes
                .Where(it =>
                    !(it.IsAbstract || it.IsInterface) &&
                    it.GetInterfaces().Any(x =>
                        x.IsGenericType &&
                        x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>)))
                .ToList();

            foreach (var item in typesToRegister)
            {
                dynamic service = Activator.CreateInstance(item);

                modelBuilder.ApplyConfiguration(service);
            }
        }

        public static void NeedToRegisterEntitiesConfig<T>(this ModelBuilder modelBuilder)
        {
            var typesToRegister = AssemblyScanner.AllTypes
                .Where(it =>
                    !(it.IsAbstract || it.IsInterface) &&
                    it.GetInterfaces().Any(x =>
                        x.IsGenericType &&
                        // !x.IsAbstract &&
                        x.GetGenericTypeDefinition() == typeof(T)))
                .ToList();

            foreach (var item in typesToRegister)
            {
                modelBuilder.Entity(item);
            }
        }

        public static void AddPluralizingTableNameConvention(this ModelBuilder modelBuilder)
        {
            Pluralizer pluralize = new Pluralizer();

            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                string tableName = entityType.GetTableName();

                entityType.SetTableName(pluralize.Pluralize(tableName));
            }
        }

        public static void AddSequentialGuidForIdConvention(this ModelBuilder modelBuilder)
        {
            modelBuilder.AddDefaultValueSqlConvention("Id", typeof(Guid), "NEWSEQUENTIALID()");
        }

        private static void AddDefaultValueSqlConvention(this ModelBuilder modelBuilder, string propertyName,
            Type propertyType, string defaultValueSql)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                IMutableProperty property = entityType.GetProperties()
                    .SingleOrDefault(p => p.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

                if (property != null && property.ClrType == propertyType)
                    property.SetDefaultValueSql(defaultValueSql);
            }
        }
    }
}