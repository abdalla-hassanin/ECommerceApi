using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class ReviewSpecifications
{
    public sealed class ByReviewId : BaseSpecification<Review>
    {
        public ByReviewId(string reviewId) : base(r => r.ReviewId == reviewId)
        {
            AddInclude(r => r.Product);
            AddInclude(r => r.Customer);
            AddInclude(r => r.Order);
        }
    }

    public sealed class ByProductId : BaseSpecification<Review>
    {
        public ByProductId(string productId) : base(r => r.ProductId == productId)
        {
            ApplyOrderByDescending(r => r.CreatedAt);
            AddInclude(r => r.Customer);
        }
    }

    public sealed class ByCustomerId : BaseSpecification<Review>
    {
        public ByCustomerId(string customerId) : base(r => r.CustomerId == customerId)
        {
            ApplyOrderByDescending(r => r.CreatedAt);
            AddInclude(r => r.Product);
        }
    }
}