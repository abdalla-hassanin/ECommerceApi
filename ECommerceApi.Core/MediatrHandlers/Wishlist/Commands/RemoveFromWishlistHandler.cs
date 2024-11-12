using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist.Commands;

public record RemoveFromWishlistCommand(string CustomerId, string ProductId) : IRequest<ApiResponse<bool>>;

public class RemoveFromWishlistCommandValidator : AbstractValidator<RemoveFromWishlistCommand>
{
    public RemoveFromWishlistCommandValidator()
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

public class RemoveFromWishlistHandler(
    IWishlistService wishlistService,
    ILogger<RemoveFromWishlistHandler> logger) 
    : IRequestHandler<RemoveFromWishlistCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(RemoveFromWishlistCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing product {ProductId} from wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);

        try
        {
            await wishlistService.DeleteWishlistItemAsync(request.CustomerId, request.ProductId, cancellationToken);
            logger.LogInformation("Product {ProductId} removed from wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);
            return ApiResponse<bool>.Factory.Success(true, "Item removed from wishlist successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while removing product {ProductId} from wishlist for customer {CustomerId}", request.ProductId, request.CustomerId);
            throw;
        }
    }
}