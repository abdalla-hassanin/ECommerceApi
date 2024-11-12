namespace ECommerceApi.Core.MediatrHandlers.ProductImage;

public record ProductImageDto(
    string ImageId,
    string ProductId,
    string? VariantId,
    string ImageUrl,
    string AltText,
    int DisplayOrder,
    DateTime CreatedAt,
    DateTime UpdatedAt
);