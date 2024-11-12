using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class PaymentSpecifications
{
    public sealed class ByOrderId(string orderId) : BaseSpecification<Payment>(p => p.OrderId == orderId);

    public sealed class ByStatus(string status) : BaseSpecification<Payment>(p => p.Status == status);
}
