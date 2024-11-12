namespace ECommerceApi.Core.MediatrHandlers.Review;

public record ReviewDto(
    string ReviewId,
    string ProductId,
    string CustomerId,
    string OrderId,
    int Rating,
    string Title,
    string Content,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);