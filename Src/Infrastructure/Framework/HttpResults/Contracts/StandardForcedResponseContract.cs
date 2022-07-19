namespace Infrastructure.Framework.HttpResults.Contracts;

public class StandardForcedResponseContract
{
    public StandardForcedResultContract Result { get; set; }
}

public class StandardForcedResponseContract<TData> : StandardForcedResponseContract
{
    public TData Data { get; set; }
}
public class StandardForcedResponseContract<TData, TAdditional> : StandardForcedResponseContract
{
    public TData Data { get; set; }
    public TAdditional Additional { get; set; }
}