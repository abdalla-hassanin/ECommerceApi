namespace ECommerceApi.Core.MediatrHandlers.Category;

public record CategoryDto(
    string CategoryId,
    string Name,
    string Description,
    string ImageUrl,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    string? ParentCategoryId
);