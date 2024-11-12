using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IWishlistService
{
    Task<Wishlist?> GetWishlistItemAsync(string customerId, string productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Wishlist>> GetWishlistForCustomerAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Wishlist> AddToWishlistAsync(Wishlist wishlistItem, CancellationToken cancellationToken = default);
    Task DeleteWishlistItemAsync(string customerId, string productId, CancellationToken cancellationToken = default);
    Task ClearWishlistAsync(string customerId, CancellationToken cancellationToken = default);
    
}