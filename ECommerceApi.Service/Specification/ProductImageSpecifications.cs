using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class ProductImageSpecifications
{
    public sealed class ByProductId : BaseSpecification<ProductImage>
    {
        public ByProductId(string productId) : base(pi => pi.ProductId == productId)
        {
            ApplyOrderBy(pi => pi.DisplayOrder);
        }
    }

    public sealed class ByVariantId : BaseSpecification<ProductImage>
    {
        public ByVariantId(string variantId) : base(pi => pi.VariantId == variantId)
        {
            ApplyOrderBy(pi => pi.DisplayOrder);
        }
    }

}