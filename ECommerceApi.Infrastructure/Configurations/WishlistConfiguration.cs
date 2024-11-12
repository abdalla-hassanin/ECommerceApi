using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class WishlistConfiguration : IEntityTypeConfiguration<Wishlist>
{
    public void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        builder.HasKey(w => w.WishlistId);
        builder.Property(w => w.WishlistId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(w => w.CustomerId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(w => w.ProductId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        
        builder.Property(w => w.CreatedAt).IsRequired();

        builder.HasIndex(w => new { w.CustomerId, w.ProductId }).IsUnique();
    }
}
