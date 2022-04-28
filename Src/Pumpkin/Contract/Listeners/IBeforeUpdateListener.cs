using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Contract.Listeners;

public interface IBeforeUpdateListener
{
    void OnBeforeUpdate(EntityEntry entry);
}