namespace Pumpkin.Contract.Listeners
{
    public interface IBeforeDeleteListener
    {
        void OnBeforeDelete(object entity);
    }
}