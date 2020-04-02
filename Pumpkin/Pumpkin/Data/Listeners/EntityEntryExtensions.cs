using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pumpkin.Contract.Listeners;

namespace Pumpkin.Data.Listeners
{
    public static class EntityEntryExtensions
    {
        public static ChangedEntity Map(this EntityEntry entity)
        {
            
            var model = new ChangedEntity();
            foreach (var item in entity.CurrentValues.Properties)
            {
                var currentValue = entity.CurrentValues[item.Name];
                if (entity.OriginalValues != null)
                {
                    var oldValue = entity.OriginalValues[item.Name];
                    if (oldValue != currentValue)
                    {
                        model.ChangedValues.Add(new ChangedValue
                        {
                            CurrentValue = currentValue,
                            Name = item.Name,
                            OldValue = oldValue
                        });
                    }
                }
                else
                {
                    model.ChangedValues.Add(new ChangedValue
                    {
                        CurrentValue = currentValue,
                        Name = item.Name,
                        OldValue = null
                    });
                }
            }
            
            model.NewState = MapState(entity.State);
            model.Entity = entity.Entity;
            return model;
        }
        private static EntityChangeState MapState(EntityState state)
        {
            switch (state)
            {
                case EntityState.Detached:
                    return EntityChangeState.Detached;
                case EntityState.Unchanged:
                    return EntityChangeState.Unchanged;
                case EntityState.Deleted:
                    return EntityChangeState.Deleted;
                case EntityState.Modified:
                    return EntityChangeState.Modified;
                case EntityState.Added:
                    return EntityChangeState.Added;
                default:
                    return EntityChangeState.Detached;
            }
        }
    }
    
}
