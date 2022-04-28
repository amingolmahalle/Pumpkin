namespace Pumpkin.Contract.Domain;

public interface IConcurrencyToken
{
    byte[] RowVersion { get; set; }
}