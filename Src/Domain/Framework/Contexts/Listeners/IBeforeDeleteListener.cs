using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Domain.Framework.Contexts.Listeners;

public interface IBeforeDeleteListener
{
    void OnBeforeDelete(EntityEntry entry);
}