using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Contexts.Listeners;

public interface IBeforeDeleteListener
{
    void OnBeforeDelete(EntityEntry entry);
}