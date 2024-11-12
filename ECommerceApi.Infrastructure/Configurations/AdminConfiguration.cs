using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class AdminConfiguration : IEntityTypeConfiguration<Admin>
{
    public void Configure(EntityTypeBuilder<Admin> builder)
    {
        builder.HasKey(a => a.AdminId);
        builder.Property(a => a.AdminId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(a => a.ApplicationUserId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(a => a.Position).IsRequired().HasMaxLength(100);
    }
}
