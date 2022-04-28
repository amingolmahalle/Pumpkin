using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SampleWebApi.Domain.Entity.UserAggregate;

namespace SampleWebApi.Data.EntityConfig;

public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Fullname)
            .HasColumnType("nvarchar(40)")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(c => c.MobileNumber)
            .HasColumnType("char(11)")
            .HasMaxLength(11)
            .IsUnicode()
            .IsRequired();

        builder.Property(c => c.Email)
            .HasColumnType("nvarchar(30)")
            .HasMaxLength(30);

        builder.Property(c => c.NationalCode)
            .HasColumnType("char(10)")
            .HasMaxLength(10);
        
        SeedData(builder);
    }

    private void SeedData(EntityTypeBuilder<User> builder)
    {
        var users = new List<User>
        {
            new()
            {
                Id = 10001,
                Fullname = "user 1",
                NationalCode = "5820005546",
                MobileNumber = "09120000001",
                Email = "user1@gmail.com",
                CreatedAt = new DateTime(2022, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                CreatedBy = 10001,
                ModifiedAt = new DateTime(2022, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                ModifiedBy = 10001,
            },
            new()
            {
                Id = 10002,
                Fullname = "user 2",
                NationalCode = "5820005545",
                MobileNumber = "09120000002",
                Email = "user2@gmail.com",
                CreatedAt = new DateTime(2022, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                CreatedBy = 10001,
                ModifiedAt = new DateTime(2022, 1, 1, 10, 0, 0, DateTimeKind.Utc),
                ModifiedBy = 10001,
            }
        };
        
        builder.HasData(users);
    }
}