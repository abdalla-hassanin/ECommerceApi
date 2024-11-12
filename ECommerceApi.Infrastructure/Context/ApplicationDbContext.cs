using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ILogger<ApplicationDbContext> _logger;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILogger<ApplicationDbContext> logger) :
        base(options)
    {
        _logger = logger;
        // try
        // {
        //     var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        //     if (databaseCreator != null)
        //     {
        //         if (!databaseCreator.CanConnect())
        //         {
        //             _logger.LogInformation("Database does not exist. Creating database");
        //             databaseCreator.Create();
        //             _logger.LogInformation("Database created");
        //         }
        //
        //         if (!databaseCreator.HasTables())
        //         {
        //             _logger.LogInformation("Database tables do not exist. Creating tables");
        //             databaseCreator.CreateTables();
        //             _logger.LogInformation("Database tables created");
        //         }
        //     }
        // }
        // catch (Exception e)
        // {
        //     _logger.LogError(e, "An error occurred while creating database");
        // }
    }

    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Coupon> Coupons { get; set; }
    public DbSet<Wishlist> WishlistItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _logger.LogInformation("Configuring database model");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());
        modelBuilder.ApplyConfiguration(new AdminConfiguration());
        modelBuilder.ApplyConfiguration(new AddressConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ProductVariantConfiguration());
        modelBuilder.ApplyConfiguration(new ProductImageConfiguration());
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new ReviewConfiguration());
        modelBuilder.ApplyConfiguration(new CouponConfiguration());
        modelBuilder.ApplyConfiguration(new WishlistConfiguration());

        // Seed Roles with specific IDs to prevent duplicates
        modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole 
            { 
                Id = Ulid.NewUlid().ToString(),
                Name = "Admin", 
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Ulid.NewUlid().ToString()
            },
            new IdentityRole 
            { 
                Id = Ulid.NewUlid().ToString(),
                Name = "Customer", 
                NormalizedName = "CUSTOMER",
                ConcurrencyStamp = Ulid.NewUlid().ToString()
            }
        );
        _logger.LogInformation("Database model configuration completed");
    }
}