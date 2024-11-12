using ECommerceApi.Core.MediatrHandlers.ProductImage;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant;

public record ProductVariantDto(
    string VariantId,
    string ProductId,
    string Name,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    int InventoryQuantity,
    int LowStockThreshold,
    decimal? Weight,
    string? WeightUnit,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    ICollection<ProductImageDto> Images
);
