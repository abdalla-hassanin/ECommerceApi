namespace ECommerceApi.Core.MediatrHandlers.Product;

public record ProductDto(
    string ProductId,
    string Name,
    string Description,
    string ShortDescription,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);