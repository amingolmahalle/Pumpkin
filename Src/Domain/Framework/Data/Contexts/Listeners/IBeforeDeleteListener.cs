using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Domain.Framework.Data.Contexts.Listeners;

public interface IBeforeDeleteListener
{
    void OnBeforeDelete(EntityEntry entry);
}