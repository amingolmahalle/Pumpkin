using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Framework.Contexts.Listeners;

public interface IBeforeInsertListener
{
    void OnBeforeInsert(EntityEntry entry);
}