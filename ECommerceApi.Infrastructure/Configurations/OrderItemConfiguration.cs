using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.OrderItemId);
        builder.Property(oi => oi.OrderItemId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(oi => oi.OrderId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(oi => oi.ProductId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(oi => oi.VariantId)
            .HasMaxLength(26); // ULID length
        
        builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
        builder.Property(oi => oi.Subtotal).HasColumnType("decimal(18,2)");
        builder.Property(oi => oi.Tax).HasColumnType("decimal(18,2)");
        builder.Property(oi => oi.Total).HasColumnType("decimal(18,2)");
        builder.Property(oi => oi.CreatedAt).IsRequired();
        builder.Property(oi => oi.UpdatedAt).IsRequired();

        builder.HasOne(oi => oi.Variant)
            .WithMany()
            .HasForeignKey(oi => oi.VariantId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Add these new configurations
        builder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
