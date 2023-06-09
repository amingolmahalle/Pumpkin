using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pumpkin.Domain.Entities.Order;
using Pumpkin.Domain.Entities.Order.Enumerations;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Infrastructure.Contexts.FluentApi.SqlServer.Order;

public class PolicyConfig : IEntityTypeConfiguration<Policy>
{
    public void Configure(EntityTypeBuilder<Policy> builder)
    {
        builder.ToTable("Policy", "dbo");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.OrderItemId)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v))
            .IsRequired();

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

        builder.Property(c => c.IssuedAt)
            .HasColumnType("DateTime")
            .IsRequired(false);

        builder.Property(c => c.StartAt)
            .HasColumnType("DateTime")
            .IsRequired(false);

        builder.Property(c => c.ExpireAt)
            .HasColumnType("DateTime")
            .IsRequired(false);

        builder.Property(c => c.IsActive)
            .HasColumnType("BIT")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(c => c.CurrentState)
            .HasColumnType("SMALLINT")
            .HasDefaultValue(PolicyStates.Pending)
            .IsRequired();
    }
}