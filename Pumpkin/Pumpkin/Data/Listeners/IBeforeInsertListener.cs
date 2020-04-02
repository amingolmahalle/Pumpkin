using Pumpkin.Contract.Listeners;

namespace Pumpkin.Data.Listeners
{
    public interface IBeforeInsertListener
    {
        void OnBeforeInsert(ChangedEntity entity);
    }
}