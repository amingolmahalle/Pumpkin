namespace Pumpkin.Contract.Domain
{
    public abstract class Entity<TId> : IEntity<TId> 
    {
        public TId Id { get; set ; }
    }
}
