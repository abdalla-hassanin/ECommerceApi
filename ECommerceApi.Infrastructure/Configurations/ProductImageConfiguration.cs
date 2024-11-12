using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ECommerceApi.Infrastructure.Configurations;

public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
{
    public void Configure(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(i => i.ImageId);
        builder.Property(i => i.ImageId)
            .IsRequired()
            .HasMaxLength(26) // ULID length
            .ValueGeneratedNever(); // ULID is generated in the entity

        builder.Property(i => i.ProductId)
            .IsRequired()
            .HasMaxLength(26);

        builder.Property(i => i.VariantId)
            .HasMaxLength(26);
        builder.Property(i => i.ImageUrl).IsRequired().HasMaxLength(255);
        builder.Property(i => i.AltText).HasMaxLength(255);
        builder.Property(i => i.CreatedAt).IsRequired();
        builder.Property(i => i.UpdatedAt).IsRequired();

        // Seed Data
        builder.HasData(
            // كشري
            new ProductImage
            {
                ImageId = ProductImageIds.KosharyMain,
                ProductId = ProductIds.KosharyClassic,
                ImageUrl = "https://foodimages.store/koshary/main.jpg",
                AltText = "كشري مصري تقليدي",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.KosharyAlt,
                ProductId = ProductIds.KosharyClassic,
                ImageUrl = "https://foodimages.store/koshary/alt.jpg",
                AltText = "مكونات الكشري المصري",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // ملوخية
            new ProductImage
            {
                ImageId = ProductImageIds.MolokhiaMain,
                ProductId = ProductIds.MolokhiaWithChicken,
                ImageUrl = "https://foodimages.store/molokhia/main.jpg",
                AltText = "ملوخية بالفراخ",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.MolokhiaAlt,
                ProductId = ProductIds.MolokhiaWithChicken,
                ImageUrl = "https://foodimages.store/molokhia/alt.jpg",
                AltText = "طبق ملوخية مع الأرز",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // فول مدمس
            new ProductImage
            {
                ImageId = ProductImageIds.FulMain,
                ProductId = ProductIds.FulMedames,
                ImageUrl = "https://foodimages.store/ful/main.jpg",
                AltText = "فول مدمس مصري",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.FulAlt,
                ProductId = ProductIds.FulMedames,
                ImageUrl = "https://foodimages.store/ful/alt.jpg",
                AltText = "فول مدمس مع الزيت والليمون",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // مشويات مشكلة
            new ProductImage
            {
                ImageId = ProductImageIds.MixedGrillMain,
                ProductId = ProductIds.MixedGrill,
                ImageUrl = "https://foodimages.store/mixedgrill/main.jpg",
                AltText = "مشويات مشكلة مصرية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.MixedGrillAlt,
                ProductId = ProductIds.MixedGrill,
                ImageUrl = "https://foodimages.store/mixedgrill/alt.jpg",
                AltText = "تشكيلة من اللحوم المشوية",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // كنافة
            new ProductImage
            {
                ImageId = ProductImageIds.KunafaMain,
                ProductId = ProductIds.Kunafa,
                ImageUrl = "https://foodimages.store/kunafa/main.jpg",
                AltText = "كنافة نابلسية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.KunafaAlt,
                ProductId = ProductIds.Kunafa,
                ImageUrl = "https://foodimages.store/kunafa/alt.jpg",
                AltText = "كنافة بالقشطة والمكسرات",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            // عيش بلدي
            new ProductImage
            {
                ImageId = ProductImageIds.BaladiMain,
                ProductId = ProductIds.BaladiBread,
                ImageUrl = "https://foodimages.store/bread/baladi-main.jpg",
                AltText = "عيش بلدي طازج",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.BaladiAlt,
                ProductId = ProductIds.BaladiBread,
                ImageUrl = "https://foodimages.store/bread/baladi-alt.jpg",
                AltText = "عيش بلدي مقطع",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // سمك قاروص
            new ProductImage
            {
                ImageId = ProductImageIds.SeabassMain,
                ProductId = ProductIds.GrilledSeabass,
                ImageUrl = "https://foodimages.store/fish/seabass-main.jpg",
                AltText = "سمك قاروص مشوي",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.SeabassAlt,
                ProductId = ProductIds.GrilledSeabass,
                ImageUrl = "https://foodimages.store/fish/seabass-alt.jpg",
                AltText = "طبق سمك قاروص مع الخضار",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // فتة باللحمة
            new ProductImage
            {
                ImageId = ProductImageIds.FattahMain,
                ProductId = ProductIds.FattahWithMeat,
                ImageUrl = "https://foodimages.store/fattah/meat-main.jpg",
                AltText = "فتة باللحمة المصرية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.FattahAlt,
                ProductId = ProductIds.FattahWithMeat,
                ImageUrl = "https://foodimages.store/fattah/meat-alt.jpg",
                AltText = "فتة مع الخبز المحمص",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // بابا غنوج
            new ProductImage
            {
                ImageId = ProductImageIds.BabaGhanoushMain,
                ProductId = ProductIds.BabaGhanoush,
                ImageUrl = "https://foodimages.store/appetizers/babaganoush-main.jpg",
                AltText = "بابا غنوج",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.BabaGhanoushAlt,
                ProductId = ProductIds.BabaGhanoush,
                ImageUrl = "https://foodimages.store/appetizers/babaganoush-alt.jpg",
                AltText = "بابا غنوج مع الخبز",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // شوربة عدس
            new ProductImage
            {
                ImageId = ProductImageIds.LentilSoupMain,
                ProductId = ProductIds.LentilSoup,
                ImageUrl = "https://foodimages.store/soups/lentil-main.jpg",
                AltText = "شوربة عدس",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.LentilSoupAlt,
                ProductId = ProductIds.LentilSoup,
                ImageUrl = "https://foodimages.store/soups/lentil-alt.jpg",
                AltText = "شوربة عدس مع الليمون",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // أرز صيادية
            new ProductImage
            {
                ImageId = ProductImageIds.SayyadiaMain,
                ProductId = ProductIds.SayyadiaRice,
                ImageUrl = "https://foodimages.store/rice/sayadia-main.jpg",
                AltText = "أرز صيادية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.SayyadiaAlt,
                ProductId = ProductIds.SayyadiaRice,
                ImageUrl = "https://foodimages.store/rice/sayadia-alt.jpg",
                AltText = "أرز صيادية مع السمك",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // مكرونة إسكندراني
            new ProductImage
            {
                ImageId = ProductImageIds.AlexPastaMain,
                ProductId = ProductIds.AlexandriaPasta,
                ImageUrl = "https://foodimages.store/pasta/alexandria-main.jpg",
                AltText = "مكرونة إسكندراني",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.AlexPastaAlt,
                ProductId = ProductIds.AlexandriaPasta,
                ImageUrl = "https://foodimages.store/pasta/alexandria-alt.jpg",
                AltText = "مكرونة إسكندراني بالجمبري",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // شاي مصري
            new ProductImage
            {
                ImageId = ProductImageIds.EgyptianTeaMain,
                ProductId = ProductIds.EgyptianTea,
                ImageUrl = "https://foodimages.store/beverages/tea-main.jpg",
                AltText = "شاي مصري",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.EgyptianTeaAlt,
                ProductId = ProductIds.EgyptianTea,
                ImageUrl = "https://foodimages.store/beverages/tea-alt.jpg",
                AltText = "شاي مصري بالنعناع",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // عصير قصب
            new ProductImage
            {
                ImageId = ProductImageIds.SugarCaneMain,
                ProductId = ProductIds.SugarCaneJuice,
                ImageUrl = "https://foodimages.store/beverages/sugarcane-main.jpg",
                AltText = "عصير قصب طازج",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.SugarCaneAlt,
                ProductId = ProductIds.SugarCaneJuice,
                ImageUrl = "https://foodimages.store/beverages/sugarcane-alt.jpg",
                AltText = "عصير قصب في كوب",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // فراخ مشوية
            new ProductImage
            {
                ImageId = ProductImageIds.GrilledChickenMain,
                ProductId = ProductIds.GrilledChicken,
                ImageUrl = "https://foodimages.store/chicken/grilled-main.jpg",
                AltText = "فراخ مشوية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.GrilledChickenAlt,
                ProductId = ProductIds.GrilledChicken,
                ImageUrl = "https://foodimages.store/chicken/grilled-alt.jpg",
                AltText = "فراخ مشوية مع الأرز",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // كفتة مشوية
            new ProductImage
            {
                ImageId = ProductImageIds.KoftaMeatMain,
                ProductId = ProductIds.KoftaMeat,
                ImageUrl = "https://foodimages.store/meat/kofta-main.jpg",
                AltText = "كفتة مشوية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.KoftaMeatAlt,
                ProductId = ProductIds.KoftaMeat,
                ImageUrl = "https://foodimages.store/meat/kofta-alt.jpg",
                AltText = "كفتة مشوية مع السلطة",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // مخلل مشكل
            new ProductImage
            {
                ImageId = ProductImageIds.MixedPicklesMain,
                ProductId = ProductIds.MixedPickles,
                ImageUrl = "https://foodimages.store/pickles/mixed-main.jpg",
                AltText = "مخلل مشكل",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.MixedPicklesAlt,
                ProductId = ProductIds.MixedPickles,
                ImageUrl = "https://foodimages.store/pickles/mixed-alt.jpg",
                AltText = "تشكيلة مخللات",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // دقة مصرية
            new ProductImage
            {
                ImageId = ProductImageIds.SpiceMixMain,
                ProductId = ProductIds.EgyptianSpiceMix,
                ImageUrl = "https://foodimages.store/spices/mix-main.jpg",
                AltText = "دقة مصرية",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.SpiceMixAlt,
                ProductId = ProductIds.EgyptianSpiceMix,
                ImageUrl = "https://foodimages.store/spices/mix-alt.jpg",
                AltText = "توابل مصرية",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // صلصة طحينة
            new ProductImage
            {
                ImageId = ProductImageIds.TahiniMain,
                ProductId = ProductIds.TahiniSauce,
                ImageUrl = "https://foodimages.store/sauces/tahini-main.jpg",
                AltText = "صلصة طحينة",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.TahiniAlt,
                ProductId = ProductIds.TahiniSauce,
                ImageUrl = "https://foodimages.store/sauces/tahini-alt.jpg",
                AltText = "طحينة مع السلطة",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },

            // حواوشي
            new ProductImage
            {
                ImageId = ProductImageIds.HawawshiMain,
                ProductId = ProductIds.Hawawshi,
                ImageUrl = "https://foodimages.store/fastfood/hawawshi-main.jpg",
                AltText = "حواوشي مصري",
                DisplayOrder = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ProductImage
            {
                ImageId = ProductImageIds.HawawshiAlt,
                ProductId = ProductIds.Hawawshi,
                ImageUrl = "https://foodimages.store/fastfood/hawawshi-alt.jpg",
                AltText = "حواوشي مع السلطة",
                DisplayOrder = 2,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );
    }
}