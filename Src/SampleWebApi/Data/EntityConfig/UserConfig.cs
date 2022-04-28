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
    }
}