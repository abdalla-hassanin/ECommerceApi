using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
{
    public void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });
        
        builder.Property(pc=> pc.ProductId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
        builder.Property(pc => pc.CategoryId)
            .IsRequired()
            .HasMaxLength(26); // ULID length
        
         // Seed Data
        builder.HasData(
            // كشري - أطباق الأرز والمكرونة
            new ProductCategory { ProductId = ProductIds.KosharyClassic, CategoryId = CategoryIds.RiceDishes },
            new ProductCategory { ProductId = ProductIds.KosharyClassic, CategoryId = CategoryIds.PastaDishes },
            
            // ملوخية - خضروات ودجاج
            new ProductCategory { ProductId = ProductIds.MolokhiaWithChicken, CategoryId = CategoryIds.VegetablesAndLegumes },
            new ProductCategory { ProductId = ProductIds.MolokhiaWithChicken, CategoryId = CategoryIds.ChickenDishes },
            
            // فول مدمس - فطور
            new ProductCategory { ProductId = ProductIds.FulMedames, CategoryId = CategoryIds.Breakfast },
            new ProductCategory { ProductId = ProductIds.FulMedames, CategoryId = CategoryIds.VegetablesAndLegumes },
            
            // مشويات مشكلة
            new ProductCategory { ProductId = ProductIds.MixedGrill, CategoryId = CategoryIds.GrilledFoods },
            new ProductCategory { ProductId = ProductIds.MixedGrill, CategoryId = CategoryIds.MeatDishes },
            
            // كنافة
            new ProductCategory { ProductId = ProductIds.Kunafa, CategoryId = CategoryIds.OrientalSweets },
            
            // عيش بلدي
            new ProductCategory { ProductId = ProductIds.BaladiBread, CategoryId = CategoryIds.Bakery },
            
            // سمك قاروص
            new ProductCategory { ProductId = ProductIds.GrilledSeabass, CategoryId = CategoryIds.Seafood },
            new ProductCategory { ProductId = ProductIds.GrilledSeabass, CategoryId = CategoryIds.GrilledFoods },
            
            // فتة باللحمة
            new ProductCategory { ProductId = ProductIds.FattahWithMeat, CategoryId = CategoryIds.MeatDishes },
            
            // بابا غنوج
            new ProductCategory { ProductId = ProductIds.BabaGhanoush, CategoryId = CategoryIds.Appetizers },
            new ProductCategory { ProductId = ProductIds.BabaGhanoush, CategoryId = CategoryIds.VegetarianDishes },
            
            // شوربة عدس
            new ProductCategory { ProductId = ProductIds.LentilSoup, CategoryId = CategoryIds.Soups },
            new ProductCategory { ProductId = ProductIds.LentilSoup, CategoryId = CategoryIds.VegetarianDishes },
            
            // أرز صيادية
            new ProductCategory { ProductId = ProductIds.SayyadiaRice, CategoryId = CategoryIds.RiceDishes },
            new ProductCategory { ProductId = ProductIds.SayyadiaRice, CategoryId = CategoryIds.Seafood },
            
            // مكرونة إسكندراني
            new ProductCategory { ProductId = ProductIds.AlexandriaPasta, CategoryId = CategoryIds.PastaDishes },
            new ProductCategory { ProductId = ProductIds.AlexandriaPasta, CategoryId = CategoryIds.Seafood },
            
            // شاي مصري
            new ProductCategory { ProductId = ProductIds.EgyptianTea, CategoryId = CategoryIds.HotBeverages },
            
            // عصير قصب
            new ProductCategory { ProductId = ProductIds.SugarCaneJuice, CategoryId = CategoryIds.NaturalJuices },
            
            // فراخ مشوية
            new ProductCategory { ProductId = ProductIds.GrilledChicken, CategoryId = CategoryIds.ChickenDishes },
            new ProductCategory { ProductId = ProductIds.GrilledChicken, CategoryId = CategoryIds.GrilledFoods },
            
            // كفتة مشوية
            new ProductCategory { ProductId = ProductIds.KoftaMeat, CategoryId = CategoryIds.MeatDishes },
            new ProductCategory { ProductId = ProductIds.KoftaMeat, CategoryId = CategoryIds.GrilledFoods },
            
            // مخلل مشكل
            new ProductCategory { ProductId = ProductIds.MixedPickles, CategoryId = CategoryIds.Pickles },
            
            // دقة مصرية
            new ProductCategory { ProductId = ProductIds.EgyptianSpiceMix, CategoryId = CategoryIds.SpicesAndSeasonings },
            
            // صلصة طحينة
            new ProductCategory { ProductId = ProductIds.TahiniSauce, CategoryId = CategoryIds.SaucesAndDips },
            
            // حواوشي
            new ProductCategory { ProductId = ProductIds.Hawawshi, CategoryId = CategoryIds.FastFood },
            new ProductCategory { ProductId = ProductIds.Hawawshi, CategoryId = CategoryIds.MeatDishes }
        );
    }
}
