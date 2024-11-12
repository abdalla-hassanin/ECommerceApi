using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class WishlistSpecifications
{
    public sealed class ByCustomerId : BaseSpecification<Wishlist>
    {
        public ByCustomerId(string customerId) : base(w => w.CustomerId == customerId)
        {
            AddInclude(w => w.Product);
            ApplyOrderByDescending(w => w.CreatedAt);
        }
    }

    public sealed class ByCustomerAndProduct : BaseSpecification<Wishlist>
    {
        public ByCustomerAndProduct(string customerId, string productId) 
            : base(w => w.CustomerId == customerId && w.ProductId == productId)
        {
            AddInclude(w => w.Product);
        }
    }
}
