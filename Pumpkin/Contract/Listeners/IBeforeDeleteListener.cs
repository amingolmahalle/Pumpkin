using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Pumpkin.Contract.Listeners
{
    public interface IBeforeDeleteListener
    {
        void OnBeforeDelete(EntityEntry entry);
    }
}