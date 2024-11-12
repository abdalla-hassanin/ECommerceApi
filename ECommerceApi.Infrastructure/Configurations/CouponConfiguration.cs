using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.HasKey(c => c.CouponId);
        builder.Property(c => c.CouponId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.DiscountType).IsRequired().HasMaxLength(20);
        builder.Property(c => c.DiscountValue).HasColumnType("decimal(18,2)");
        builder.Property(c => c.MinimumPurchase).HasColumnType("decimal(18,2)");
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();

        builder.HasIndex(c => c.Code).IsUnique();
    }
}
