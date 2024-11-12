using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class OrderSpecifications
{
    public sealed class GetOrderWithDetails : BaseSpecification<Order>
    {
        public GetOrderWithDetails(string? orderId = null, string? customerId = null, string? status = null) : base
        (o =>
            (string.IsNullOrEmpty(orderId) || o.OrderId == orderId) &&
            (string.IsNullOrEmpty(customerId) || o.CustomerId == customerId) &&
            (string.IsNullOrEmpty(status) || o.Status == status))
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.ShippingAddress);
            AddInclude(o => o.Coupon);
            AddInclude(o => o.Payment);
            AddInclude(o => o.OrderItems);
            AddInclude("OrderItems.Product");
            AddInclude("OrderItems.Variant");
        }
    }
    public sealed class GetOrderById(string orderId) : BaseSpecification<Order>(o => o.OrderId == orderId)
    {
        public GetOrderById() : this("") // Parameterless constructor
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.ShippingAddress);
            AddInclude(o => o.Coupon);
            AddInclude(o => o.Payment);
            AddInclude(o => o.OrderItems);
            AddInclude("OrderItems.Product");
            AddInclude("OrderItems.Variant");
        }
    }

    public sealed class GetOrdersByCustomerId(string customerId) : BaseSpecification<Order>(o => o.CustomerId == customerId)
    {
        public GetOrdersByCustomerId() : this("") // Parameterless constructor
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(o => o.OrderItems);
            AddInclude("OrderItems.Product");
        }
    }

    public sealed class GetOrdersByStatus(string status) : BaseSpecification<Order>(o => o.Status == status)
    {
        public GetOrdersByStatus() : this(string.Empty) // Parameterless constructor
        {
            AddIncludes();
        }

        private void AddIncludes()
        {
            AddInclude(o => o.Customer);
            AddInclude(o => o.OrderItems);
        }
    }

}
