using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist.Commands;

public record AddToWishlistCommand(
    string CustomerId,
    string ProductId
) : IRequest<ApiResponse<WishlistDto>>;

public class AddToWishlistCommandValidator : AbstractValidator<AddToWishlistCommand>
{
    public AddToWishlistCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .Length(26)
            .WithMessage("CustomerId must be a valid ULID.");
            
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ProductId must be a valid ULID.");
    }
}

public class AddToWishlistHandler(
    IWishlistService wishlistService,
    IMapper mapper,
    ILogger<AddToWishlistHandler> logger) 
    : IRequestHandler<AddToWishlistCommand, ApiResponse<WishlistDto>>
{
    public async Task<ApiResponse<WishlistDto>> Handle(AddToWishlistCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding product {ProductId} to wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);

        try
        {
            var wishlistItem = mapper.Map<Data.Entities.Wishlist>(request);
            logger.LogDebug("Mapped AddToWishlistCommand to Wishlist entity");

            var addedItem = await wishlistService.AddToWishlistAsync(wishlistItem, cancellationToken);
            logger.LogInformation("Product {ProductId} added to wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);

            var wishlistDto = mapper.Map<WishlistDto>(addedItem);
            logger.LogDebug("Mapped added Wishlist entity to WishlistDto");

            return ApiResponse<WishlistDto>.Factory.Created(wishlistDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding product {ProductId} to wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);
            throw;
        }
    }
}