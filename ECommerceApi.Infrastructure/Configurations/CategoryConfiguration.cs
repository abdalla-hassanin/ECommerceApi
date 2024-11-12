using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);
        builder.Property(c => c.CategoryId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity

        builder.Property(c => c.ParentCategoryId).HasMaxLength(26); // ULID length
        builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Description).HasMaxLength(500);
        builder.Property(c => c.ImageUrl).HasMaxLength(255);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();

        builder.HasOne(c => c.ParentCategory)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.ProductCategories)
            .WithOne(pc => pc.Category)
            .HasForeignKey(pc => pc.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new Category
            {
                CategoryId = CategoryIds.TraditionalEgyptianFood,
                Name = "مأكولات مصرية تقليدية",
                Description = "أشهر الأطباق المصرية التقليدية والشعبية",
                ImageUrl = "https://foodimages.store/egyptian/traditional.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new Category
            {
                CategoryId = CategoryIds.GrilledFoods,
                Name = "مشويات",
                Description = "تشكيلة متنوعة من المشويات المصرية",
                ImageUrl = "https://foodimages.store/grilled/meats.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.OrientalSweets,
                Name = "حلويات شرقية",
                Description = "حلويات مصرية تقليدية وعربية",
                ImageUrl = "https://foodimages.store/sweets/oriental.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Bakery,
                Name = "مخبوزات",
                Description = "الخبز والمعجنات المصرية",
                ImageUrl = "https://foodimages.store/bakery/bread.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Seafood,
                Name = "مأكولات بحرية",
                Description = "أطباق السمك والمأكولات البحرية",
                ImageUrl = "https://foodimages.store/seafood/fish.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Breakfast,
                Name = "وجبات فطور",
                Description = "أطباق الفطور المصرية التقليدية",
                ImageUrl = "https://foodimages.store/breakfast/traditional.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Appetizers,
                Name = "مقبلات",
                Description = "المقبلات والسلطات المصرية",
                ImageUrl = "https://foodimages.store/appetizers/salads.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Soups,
                Name = "حساء وشوربة",
                Description = "أنواع الحساء والشوربة المصرية",
                ImageUrl = "https://foodimages.store/soups/traditional.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.RiceDishes,
                Name = "أطباق الأرز",
                Description = "تشكيلة متنوعة من أطباق الأرز",
                ImageUrl = "https://foodimages.store/rice/dishes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.PastaDishes,
                Name = "مكرونة وعجائن",
                Description = "أطباق المكرونة والعجائن المصرية",
                ImageUrl = "https://foodimages.store/pasta/dishes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.VegetablesAndLegumes,
                Name = "خضروات وبقوليات",
                Description = "أطباق الخضروات والبقوليات المصرية",
                ImageUrl = "https://foodimages.store/vegetables/legumes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.HotBeverages,
                Name = "مشروبات ساخنة",
                Description = "المشروبات الساخنة التقليدية",
                ImageUrl = "https://foodimages.store/beverages/hot.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.NaturalJuices,
                Name = "عصائر طبيعية",
                Description = "العصائر الطبيعية والمشروبات الباردة",
                ImageUrl = "https://foodimages.store/beverages/juices.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.ChickenDishes,
                Name = "أطباق الدجاج",
                Description = "تشكيلة متنوعة من أطباق الدجاج",
                ImageUrl = "https://foodimages.store/chicken/dishes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.MeatDishes,
                Name = "أطباق اللحوم",
                Description = "أطباق اللحوم المصرية المتنوعة",
                ImageUrl = "https://foodimages.store/meat/dishes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.VegetarianDishes,
                Name = "وجبات نباتية",
                Description = "أطباق نباتية مصرية صحية",
                ImageUrl = "https://foodimages.store/vegetarian/dishes.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.Pickles,
                Name = "مخللات",
                Description = "المخللات المصرية التقليدية",
                ImageUrl = "https://foodimages.store/pickles/traditional.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.SpicesAndSeasonings,
                Name = "توابل وبهارات",
                Description = "التوابل والبهارات المصرية",
                ImageUrl = "https://foodimages.store/spices/traditional.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.SaucesAndDips,
                Name = "صلصات ومقبلات",
                Description = "الصلصات والمقبلات المصرية",
                ImageUrl = "https://foodimages.store/sauces/dips.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            },
            new Category
            {
                CategoryId = CategoryIds.FastFood,
                Name = "وجبات سريعة",
                Description = "الوجبات السريعة المصرية",
                ImageUrl = "https://foodimages.store/fastfood/egyptian.jpg",
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                ParentCategoryId = CategoryIds.TraditionalEgyptianFood
            }
        );
    }
}