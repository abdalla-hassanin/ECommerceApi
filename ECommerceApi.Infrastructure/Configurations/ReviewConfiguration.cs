using ECommerceApi.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.HasKey(r => r.ReviewId);
        builder.Property(r => r.ReviewId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity
        
        builder.Property(r => r.ProductId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(r => r.CustomerId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(r => r.OrderId)
            .HasMaxLength(26); // ULID length
        
        builder.Property(r => r.Title).IsRequired().HasMaxLength(255);
        builder.Property(r => r.Content).IsRequired().HasMaxLength(2000);
        builder.Property(r => r.Status).IsRequired().HasMaxLength(50);
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.Property(r => r.UpdatedAt).IsRequired();

        builder.HasOne(r => r.Product)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Customer)
            .WithMany(c => c.Reviews)
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(r => r.Order)
            .WithMany()
            .HasForeignKey(r => r.OrderId)
            .OnDelete(DeleteBehavior.Restrict);    }
}
