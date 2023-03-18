using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Data.Contexts.Listeners;

public interface IBeforeUpdateListener
{
    void OnBeforeUpdate(EntityEntry entry);
}