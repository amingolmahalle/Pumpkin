using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Contract.Listeners
{
    public interface IBeforeInsertListener
    {
        void OnBeforeInsert(EntityEntry entry);
    }
}