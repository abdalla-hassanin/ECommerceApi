using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class ProductCategorySpecifications
{
    public sealed class CategoriesForProduct : BaseSpecification<ProductCategory>
    {
        public CategoriesForProduct(string productId)
            : base(pc => pc.ProductId == productId)
        {
            AddInclude(pc => pc.Category);
        }
    }

    public sealed class ProductsForCategory : BaseSpecification<ProductCategory>
    {
        public ProductsForCategory(string categoryId)
            : base(pc => pc.CategoryId == categoryId)
        {
            AddInclude(pc => pc.Product);
        }
    }

    public sealed class ProductInCategory(string productId, string categoryId)
        : BaseSpecification<ProductCategory>(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

    public sealed class ForProduct(string productId) : BaseSpecification<ProductCategory>(pc => pc.ProductId == productId);
}