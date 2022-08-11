using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Contexts.Listeners;

public interface IBeforeInsertListener
{
    void OnBeforeInsert(EntityEntry entry);
}