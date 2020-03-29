namespace CoreService.Framework.Contracts.Domain
{
    public abstract class Entity<T> : Entity<T,int>, IEntity<T> where T : class
    {
    }
    
    public abstract class Entity<T,TId> : IEntity<T, TId> where T : class
    {
        public TId Id { get; set ; }
    }
}
