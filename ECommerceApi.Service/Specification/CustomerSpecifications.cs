using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class CustomerSpecifications
{
    public sealed class ByCustomerId : BaseSpecification<Customer>
    {
        public ByCustomerId(string customerId) : base(c => c.CustomerId == customerId)
        {
            AddInclude(c => c.ApplicationUser);
            AddInclude(c => c.Addresses);
            AddInclude(c => c.Orders);
            AddInclude(c => c.Reviews);
            AddInclude(c => c.WishlistItems);
        }
    }
    public sealed class ByUserId : BaseSpecification<Customer>
    {
        public ByUserId(string userId) : base(a => a.ApplicationUser.Id == userId)
        {
            AddInclude(a => a.ApplicationUser);
        }
    }
    public sealed class AllCustomers : BaseSpecification<Customer>
    {
        public AllCustomers() : base(c => true)
        {
            AddInclude(c => c.ApplicationUser);
            ApplyOrderBy(c => c.CustomerId);
        }
    }
}