using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.OrderId);
        builder.Property(o => o.OrderId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(o => o.CustomerId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(o => o.ShippingAddressId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(o => o.CouponId)
            .HasMaxLength(26); // ULID length
        builder.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
        builder.Property(o => o.Status).IsRequired().HasMaxLength(50);
        builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Tax).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Shipping).HasColumnType("decimal(18,2)");
        builder.Property(o => o.Total).HasColumnType("decimal(18,2)");
        builder.Property(o => o.PaymentMethod).HasMaxLength(50);
        builder.Property(o => o.ShippingMethod).HasMaxLength(50);
        builder.Property(o => o.Notes).HasMaxLength(1000);
        builder.Property(o => o.CreatedAt).IsRequired();
        builder.Property(o => o.UpdatedAt).IsRequired();

        builder.HasOne(o => o.ShippingAddress)
            .WithMany()
            .HasForeignKey(o => o.ShippingAddressId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Coupon)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CouponId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Payment)
            .WithOne(p => p.Order)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
