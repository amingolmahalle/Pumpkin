using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pumpkin.Domain.Entities.Order;
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

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Domain.Entities.Order.Order> builder)
    {
        builder.HasData(new List<Domain.Entities.Order.Order>
        {
            new()
            {
                Id = EntityUuid.FromString("4a204175-8627-42c4-827c-b32ce6ad448f"),
                BasketCode = "1001",
                CustomerId = EntityUuid.FromString("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"),
                CustomerFirstName = "Amin",
                CustomerLastName = "Golmahalleh",
                CustomerMobileNumber = "09365545252",
                CustomerNationalCode = "1111111111",
                CustomerAddress = "Tehran, Azadi Street",
                OrderDate = DateTime.Now,
                IsConfirmed = false,
                PaymentState = PaymentStates.Unknown,
                IsPaid = false,
                CurrentState = OrderStates.Pending,
                PaymentTrackingCode = null,
                CancelDeadline = DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now,
                ModifiedAt = null,
                RemovedAt = null,
                CreatedBy = EntityUuid.FromString("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"),
                ModifiedBy = null,
                RemovedBy = null,
                TotalProductPrice = EntityAmount.SetCurrency(1000000),
                OrderItems = new List<OrderItem>
                {
                    new()
                    {
                        Id = EntityUuid.FromString("f97d0d58-b6d6-4452-809c-4946314ad16e"),
                        OrderId = EntityUuid.FromString("4a204175-8627-42c4-827c-b32ce6ad448f"),
                        BasketItemCode = "1010",
                        ProductCategory = "Mobile",
                        ProductBrand = "Samsung",
                        ProductModel = "A3 pro",
                        ProductPrice = EntityAmount.SetCurrency(1000000),
                        DeviceSerialNumber = null,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = null,
                        RemovedAt = null,
                        CreatedBy = EntityUuid.FromString("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"),
                        ModifiedBy = null,
                        RemovedBy = null,
                    }
                }
            },
            new()
            {
                Id = EntityUuid.FromString("3a94f7d5-c621-4713-8c5a-656f12ba43b1"),
                BasketCode = "1002",
                CustomerId = EntityUuid.FromString("89f5eb32-f9de-421b-9b0a-3e6cf30e7a54"),
                CustomerFirstName = "Jason",
                CustomerLastName = "Momoa",
                CustomerMobileNumber = "09123456789",
                CustomerNationalCode = "99999999999",
                CustomerAddress = "Westerville, 2044 Winding Way Street",
                OrderDate = DateTime.Now,
                IsConfirmed = false,
                PaymentState = PaymentStates.Unknown,
                IsPaid = false,
                CurrentState = OrderStates.Pending,
                PaymentTrackingCode = null,
                CancelDeadline = DateTime.Now.AddDays(7),
                CreatedAt = DateTime.Now,
                ModifiedAt = null,
                RemovedAt = null,
                CreatedBy = EntityUuid.FromString("89f5eb32-f9de-421b-9b0a-3e6cf30e7a54"),
                ModifiedBy = null,
                RemovedBy = null,
                TotalProductPrice = EntityAmount.SetCurrency(1000000),
                OrderItems = new List<OrderItem>
                {
                    new()
                    {
                        Id = EntityUuid.FromString("1df0ccc0-9118-44cc-92c2-e7b75909a993"),
                        OrderId = EntityUuid.FromString("3a94f7d5-c621-4713-8c5a-656f12ba43b1"),
                        BasketItemCode = "2010",
                        ProductCategory = "Tablet",
                        ProductBrand = "Sony",
                        ProductModel = "VivoBook 12Plus",
                        ProductPrice = EntityAmount.SetCurrency(30000000),
                        DeviceSerialNumber = null,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = null,
                        RemovedAt = null,
                        CreatedBy = EntityUuid.FromString("89f5eb32-f9de-421b-9b0a-3e6cf30e7a54"),
                        ModifiedBy = null,
                        RemovedBy = null,
                    },
                    new()
                    {
                        Id = EntityUuid.FromString("cbea4b13-36eb-4f10-8a7d-f5d36cc2bc0d"),
                        OrderId = EntityUuid.FromString("3a94f7d5-c621-4713-8c5a-656f12ba43b1"),
                        BasketItemCode = "2020",
                        ProductCategory = "Console",
                        ProductBrand = "PlayStation",
                        ProductModel = "pro 5 1Tb SSD",
                        ProductPrice = EntityAmount.SetCurrency(40000000),
                        DeviceSerialNumber = null,
                        CreatedAt = DateTime.Now,
                        ModifiedAt = null,
                        RemovedAt = null,
                        CreatedBy = EntityUuid.FromString("89f5eb32-f9de-421b-9b0a-3e6cf30e7a54"),
                        ModifiedBy = null,
                        RemovedBy = null,
                    }
                }
            }
        });
    }
}