using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pumpkin.Domain.Entities.Order;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Infrastructure.Contexts.FluentApi.SqlServer.Order;

public class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem", "dbo");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.OrderId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

        builder.Property(c => c.BasketItemCode)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.ProductCategory)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.ProductBrand)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.ProductModel)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.DeviceSerialNumber)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20)
            .IsRequired(false);

        builder.Property(c => c.ProductPrice)
            .HasConversion(a => a.Amount, a => EntityAmount.Set(a))
            .HasColumnType("DECIMAL(18,0)")
            .IsRequired();

        builder.HasOne(c => c.Order)
            .WithMany(c => c.OrderItems)
            .HasForeignKey(c => c.OrderId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("fk_order_orderItem");

        builder.HasOne(s => s.Policy)
            .WithOne(ad => ad.OrderItem)
            .HasForeignKey<Policy>(ad => ad.OrderItemId)
            .HasConstraintName("fk_Policy_orderItem");
    }
}