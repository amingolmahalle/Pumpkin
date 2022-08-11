using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Contexts.Listeners;

public interface IBeforeUpdateListener
{
    void OnBeforeUpdate(EntityEntry entry);
}