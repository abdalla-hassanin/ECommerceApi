using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist.Commands;

public record ClearWishlistCommand(string CustomerId) : IRequest<ApiResponse<bool>>;

public class ClearWishlistCommandValidator : AbstractValidator<ClearWishlistCommand>
{
    public ClearWishlistCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .Length(26)
            .WithMessage("CustomerId must be a valid ULID.");
    }
}

public class ClearWishlistHandler(
    IWishlistService wishlistService,
    ILogger<ClearWishlistHandler> logger) 
    : IRequestHandler<ClearWishlistCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ClearWishlistCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Clearing wishlist for customer {CustomerId}", request.CustomerId);

        try
        {
            await wishlistService.ClearWishlistAsync(request.CustomerId, cancellationToken);
            logger.LogInformation("Wishlist cleared successfully for customer {CustomerId}", request.CustomerId);
            return ApiResponse<bool>.Factory.Success(true, "Wishlist cleared successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while clearing wishlist for customer {CustomerId}", request.CustomerId);
            throw;
        }
    }
}