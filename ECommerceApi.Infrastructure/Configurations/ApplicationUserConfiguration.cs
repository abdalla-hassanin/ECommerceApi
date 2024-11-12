using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Id)
            .HasMaxLength(26)
            .ValueGeneratedNever(); // ULID will be generated in the constructor

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.UserName).IsUnique();
        builder.Property(u => u.FirstName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.LastName).IsRequired().HasMaxLength(50);
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired();
        builder.Property(u => u.RefreshToken).HasMaxLength(256);

        builder.HasOne(u => u.Customer)
            .WithOne(c => c.ApplicationUser)
            .HasForeignKey<Customer>(c => c.ApplicationUserId);

        builder.HasOne(u => u.Admin)
            .WithOne(a => a.ApplicationUser)
            .HasForeignKey<Admin>(a => a.ApplicationUserId);
    }
}
