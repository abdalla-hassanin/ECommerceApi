using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class CouponSpecifications
{
    public sealed class ActiveOnly() : BaseSpecification<Coupon>(c => c.IsActive && (!c.EndDate.HasValue || c.EndDate > DateTime.UtcNow));

    public sealed class ByCode(string code) : BaseSpecification<Coupon>(c => c.Code == code);
}
