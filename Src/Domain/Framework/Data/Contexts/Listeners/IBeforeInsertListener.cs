using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Data.Contexts.Listeners;

public interface IBeforeInsertListener
{
    void OnBeforeInsert(EntityEntry entry);
}