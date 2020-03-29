namespace Pumpkin.Contract.Interfaces
{
    public interface IEntity<T,TId> : IDataModel where T : class
    {
        TId Id { get; set; }
    }
    public interface IEntity<T> : IEntity<T, int>  where T : class
    {
    }

    public interface IDataModel
    {
    }
}
