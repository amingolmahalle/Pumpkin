namespace Infrastructure.Framework.HttpResults.Contracts;

public class StandardResponseContract<TData> : StandardForcedResponseContract
{
    public IEnumerable<TData> Data { get; set; }
    public Paging Paging { get; set; }
}