using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pumpkin.Domain.Framework.ValueObjects;

namespace Pumpkin.Infrastructure.Contexts.FluentApi.SqlServer.Profile.User;

public class UserConfig : IEntityTypeConfiguration<Domain.Entities.Profile.User>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Profile.User> builder)
    {
        builder.ToTable("User", "dbo");

        builder.HasKey(c => c.Id);

        builder
            .Property(c => c.Id)
            .HasConversion(v => v.Value, v => EntityUuid.FromGuid(v));

        builder.Property(c => c.FirstName)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.LastName)
            .HasColumnType("VARCHAR(20)")
            .HasMaxLength(20);

        builder.Property(c => c.MobileNumber)
            .HasColumnType("CHAR(11)")
            .HasMaxLength(11);

        builder.Property(c => c.NationalCode)
            .HasColumnType("VARCHAR(10)")
            .HasMaxLength(10)
            .IsRequired(false);

        builder.Property(c => c.Address)
            .HasColumnType("VARCHAR(50)")
            .HasMaxLength(50)
            .IsRequired(false);

        builder.Property(c => c.Gender)
            .HasColumnType("BIT")
            .IsRequired();
    }
}