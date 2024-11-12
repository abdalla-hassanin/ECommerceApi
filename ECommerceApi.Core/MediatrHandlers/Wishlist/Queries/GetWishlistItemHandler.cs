using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist.Queries;

public record GetWishlistItemQuery(string CustomerId, string ProductId) : IRequest<ApiResponse<WishlistDto>>;

public class GetWishlistItemHandler(
    IWishlistService wishlistService,
    IMapper mapper,
    ILogger<GetWishlistItemHandler> logger) 
    : IRequestHandler<GetWishlistItemQuery, ApiResponse<WishlistDto>>
{
    public async Task<ApiResponse<WishlistDto>> Handle(GetWishlistItemQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting wishlist item for customer {CustomerId} and product {ProductId}", request.CustomerId, request.ProductId);

        try
        {
            var wishlistItem = await wishlistService.GetWishlistItemAsync(request.CustomerId, request.ProductId, cancellationToken);
            if (wishlistItem is null)
            {
                logger.LogWarning("Wishlist item not found for customer {CustomerId} and product {ProductId}", request.CustomerId, request.ProductId);
                return ApiResponse<WishlistDto>.Factory.NotFound("Wishlist item not found");
            }

            var wishlistDto = mapper.Map<WishlistDto>(wishlistItem);
            logger.LogDebug("Mapped Wishlist entity to WishlistDto");

            return ApiResponse<WishlistDto>.Factory.Success(wishlistDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting wishlist item for customer {CustomerId} and product {ProductId}", request.CustomerId, request.ProductId);
            throw;
        }
    }
}