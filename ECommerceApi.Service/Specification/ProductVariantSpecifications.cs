using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class ProductVariantSpecifications
{
    public sealed class ByVariantId: BaseSpecification<ProductVariant>
    {
        public ByVariantId(string variantId) : base(pv => pv.VariantId == variantId)
        {
            AddInclude(pv => pv.Images);
        }
    }
    public sealed class ByProductId : BaseSpecification<ProductVariant>
    {
        public ByProductId(string productId) : base(pv => pv.ProductId == productId)
        {
            ApplyOrderBy(pv => pv.Name);
            AddInclude(pv => pv.Images);
        }
    }
}
