using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Infrastructure.Contexts.FluentApi.SqlServer.Order;

public class OrderConfig : IEntityTypeConfiguration<Domain.Entities.Order.Order>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
    {
        builder.ToTable("Order", "dbo");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.BasketCode)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.PaymentTrackingCode)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.CustomerFirstName)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.CustomerLastName)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.CustomerMobileNumber)
            .HasColumnType("CHAR(11)")
            .HasMaxLength(11);

        builder.Property(c => c.CustomerNationalCode)
            .HasColumnType("VARCHAR(10)")
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(c => c.CustomerAddress)
            .HasColumnType("VARCHAR(50)")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(c => c.CustomerId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.CurrentState)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(OrderStates.Pending)
            .IsRequired();

        builder.Property(c => c.PaymentState)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(PaymentStates.Unknown)
            .IsRequired();

        builder.Property(c => c.OrderDate)
            .HasColumnType("DateTime")
            .IsRequired();

        builder.Property(c => c.IsConfirmed)
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(c => c.IsPaid)
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(c => c.TotalProductPrice)
            .HasConversion(a => a.Amount, a => EntityAmount.Set(a))
            .HasColumnType("DECIMAL(18,0)")
            .IsRequired();

        // builder.MapAuditableColumns(); 
        
        builder.HasIndex(x => x.BasketCode).IsUnique();

       // SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
    {
        builder.HasData(new List<Domain.Entities.Order.Order>
        {
        });
    }
}