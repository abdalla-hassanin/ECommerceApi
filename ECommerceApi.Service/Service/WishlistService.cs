using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class WishlistService : IWishlistService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<WishlistService> _logger;

    public WishlistService(IUnitOfWork unitOfWork, ILogger<WishlistService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Wishlist?> GetWishlistItemAsync(string customerId, string productId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting wishlist item for customer ID: {CustomerId} and product ID: {ProductId}", customerId, productId);
        var spec = new WishlistSpecifications.ByCustomerAndProduct(customerId, productId);
        var wishlistItems = await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken);
        return wishlistItems.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Wishlist>> GetWishlistForCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting wishlist for customer ID: {CustomerId}", customerId);
        var spec = new WishlistSpecifications.ByCustomerId(customerId);
        return await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken);
    }

    public async Task<Wishlist> AddToWishlistAsync(Wishlist wishlistItem, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding product ID: {ProductId} to wishlist for customer ID: {CustomerId}", wishlistItem.ProductId, wishlistItem.CustomerId);
        await _unitOfWork.Repository<Wishlist>().AddAsync(wishlistItem, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Product added to wishlist successfully");
        return wishlistItem;
    }

    public async Task DeleteWishlistItemAsync(string customerId, string productId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting wishlist item for customer ID: {CustomerId} and product ID: {ProductId}", customerId, productId);
        var wishlistItem = await GetWishlistItemAsync(customerId, productId, cancellationToken);
        if (wishlistItem is not null)
        {
            await _unitOfWork.Repository<Wishlist>().DeleteAsync(wishlistItem, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Wishlist item deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent wishlist item for customer ID: {CustomerId} and product ID: {ProductId}", customerId, productId);
        }
    }

    public async Task ClearWishlistAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Clearing wishlist for customer ID: {CustomerId}", customerId);
        var spec = new WishlistSpecifications.ByCustomerId(customerId);
        var wishlistItems = await _unitOfWork.Repository<Wishlist>().ListAsync(spec, cancellationToken);
        foreach (var item in wishlistItems)
        {
            await _unitOfWork.Repository<Wishlist>().DeleteAsync(item, cancellationToken);
        }
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Wishlist cleared successfully for customer ID: {CustomerId}", customerId);
    }
}