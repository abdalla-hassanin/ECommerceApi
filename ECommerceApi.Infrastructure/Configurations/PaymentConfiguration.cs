using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.HasKey(p => p.PaymentId);
        builder.Property(p => p.PaymentId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(p => p.OrderId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
        builder.Property(p => p.Amount).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();
        builder.Property(p => p.StripePaymentIntentId).HasMaxLength(255);
        builder.Property(p => p.StripeClientSecret).HasMaxLength(255);
    }
}
