namespace Pumpkin.Contract.Interfaces
{
    public interface IConcurrencyToken
    {
        byte[] RowVersion { get; set; }
    }
}