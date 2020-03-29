namespace CoreService.Framework.Contracts.Domain
{
    public interface IConcurrencyToken
    {
        byte[] RowVersion { get; set; }
    }
}