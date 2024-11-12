namespace ECommerceApi.Core.MediatrHandlers.Order;

public record OrderDto(
    string OrderId,
    string CustomerId,
    string OrderNumber,
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal Shipping,
    decimal Total,
    string ShippingAddressId,
    string PaymentMethod,
    string ShippingMethod,
    string? CouponId,
    string? Notes,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    ICollection<OrderItemDto> OrderItems
);