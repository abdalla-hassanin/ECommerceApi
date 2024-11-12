using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist.Queries;

public record GetWishlistForCustomerQuery(string CustomerId) : IRequest<ApiResponse<IReadOnlyList<WishlistDto>>>;

public class GetWishlistForCustomerHandler(
    IWishlistService wishlistService,
    IMapper mapper,
    ILogger<GetWishlistForCustomerHandler> logger) 
    : IRequestHandler<GetWishlistForCustomerQuery, ApiResponse<IReadOnlyList<WishlistDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<WishlistDto>>> Handle(GetWishlistForCustomerQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting wishlist for customer {CustomerId}", request.CustomerId);

        try
        {
            var wishlistItems = await wishlistService.GetWishlistForCustomerAsync(request.CustomerId, cancellationToken);
            logger.LogDebug("Retrieved {Count} wishlist items for customer {CustomerId}", wishlistItems.Count, request.CustomerId);

            var wishlistDtos = mapper.Map<IReadOnlyList<WishlistDto>>(wishlistItems);
            logger.LogDebug("Mapped Wishlist entities to WishlistDto list");

            return ApiResponse<IReadOnlyList<WishlistDto>>.Factory.Success(wishlistDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting wishlist for customer {CustomerId}", request.CustomerId);
            throw;
        }
    }
}