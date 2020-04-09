namespace Pumpkin.Contract.Domain
{
    public interface IEntity<TId> 
    {
        TId Id { get; set; }
    }
}
