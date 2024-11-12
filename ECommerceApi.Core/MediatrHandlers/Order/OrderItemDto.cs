namespace ECommerceApi.Core.MediatrHandlers.Order;

public record OrderItemDto(
    string OrderItemId,
    string OrderId,
    string ProductId,
    string? VariantId,
    int Quantity,
    decimal Price,
    decimal Subtotal,
    decimal Tax,
    decimal Total,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
