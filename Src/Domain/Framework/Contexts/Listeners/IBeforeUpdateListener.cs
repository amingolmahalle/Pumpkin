using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Framework.Contexts.Listeners;

public interface IBeforeUpdateListener
{
    void OnBeforeUpdate(EntityEntry entry);
}