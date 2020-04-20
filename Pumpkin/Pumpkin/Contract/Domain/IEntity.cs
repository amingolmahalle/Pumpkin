namespace Pumpkin.Contract.Domain
{
    public interface IEntity<TId> : IEntity
    {
        TId Id { get; set; }
    }

    public interface IEntity
    {
    }
}
