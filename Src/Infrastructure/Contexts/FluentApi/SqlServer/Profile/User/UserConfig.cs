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

        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<Domain.Entities.Profile.User> builder)
    {
        builder.HasData(new List<Domain.Entities.Profile.User>
        {
            new()
            {
                Id = EntityUuid.FromString("ff9ca67a1-9bbc-4889-8fa7-21c4847fa51f"),
                FirstName = "Amin",
                LastName = "Golmahalleh",
                MobileNumber = "09365545252",
                NationalCode = "1111111111",
                Address = "Tehran, Azadi Street",
                Gender = true,
                CreatedAt = DateTime.Now,
                ModifiedAt = null,
                RemovedAt = null,
                CreatedBy = EntityUuid.FromString("f9ca67a1-9bbc-4889-8fa7-21c4847fa51f"),
                ModifiedBy = null,
                RemovedBy = null,
            },
            new()
            {
                Id = EntityUuid.FromString("3a94f7d5-c621-4713-8c5a-656f12ba43b1"),
                FirstName = "Jason",
                LastName = "Momoa",
                MobileNumber = "09123456789",
                NationalCode = "99999999999",
                Address = "Westerville, 2044 Winding Way Street",
                Gender = true,
                CreatedAt = DateTime.Now,
                ModifiedAt = null,
                RemovedAt = null,
                CreatedBy = EntityUuid.FromString("3a94f7d5-c621-4713-8c5a-656f12ba43b1"),
                ModifiedBy = null,
                RemovedBy = null,
            }
        });
    }
}