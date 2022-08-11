namespace Pumpkin.Domain.Framework.ValueObjects;

public sealed record EntityAmount(decimal Amount) : ValueObject
{
    public static EntityAmount SetCurrency(decimal amount)
        => new EntityAmount(Math.Ceiling(amount / 10000) * 10000);

    public static EntityAmount SetNullableCurrency(decimal? amount)
        => !amount.HasValue ? null : new EntityAmount(Math.Ceiling(amount.Value / 10000) * 10000);

    public static EntityAmount Set(decimal amount)
        => new EntityAmount(amount);

    public static EntityAmount SetNullable(decimal? amount)
        => !amount.HasValue ? null : new EntityAmount(amount.Value);

    public static EntityAmount Zero => new(decimal.Zero);

    public static implicit operator decimal(EntityAmount self) => self?.Amount ?? 0;
    public static implicit operator long(EntityAmount self) => (long) (self?.Amount ?? 0);
    public static implicit operator long?(EntityAmount self) => (long?) self?.Amount;
}