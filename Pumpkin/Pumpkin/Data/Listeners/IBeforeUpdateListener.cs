using Pumpkin.Contract.Listeners;

namespace Pumpkin.Data.Listeners
{
    public interface IBeforeUpdateListener
    {
        void OnBeforeUpdate(ChangedEntity entity);
    }
}
