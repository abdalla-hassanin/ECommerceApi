namespace ECommerceApi.Core.MediatrHandlers.Wishlist;

public record WishlistDto(
    string WishlistId,
    string CustomerId,
    string ProductId,
    DateTime CreatedAt
);