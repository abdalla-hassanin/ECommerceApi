using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory.Commands;

public record RemoveProductFromCategoryCommand(string ProductId, string CategoryId) : IRequest<ApiResponse<bool>>;

public class RemoveProductFromCategoryCommandValidator : AbstractValidator<RemoveProductFromCategoryCommand>
{
    public RemoveProductFromCategoryCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.")
            .Length(26).WithMessage("Product ID must be a valid ULID (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("Product ID must be a valid ULID format.");
        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category ID is required.")
            .Length(26).WithMessage("Category ID must be a valid ULID (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("Category ID must be a valid ULID format.");
        
    }
}

public class RemoveProductFromCategoryHandler(
    IProductCategoryService productCategoryService,
    ILogger<RemoveProductFromCategoryHandler> logger) : IRequestHandler<RemoveProductFromCategoryCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(RemoveProductFromCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing product {ProductId} from category {CategoryId}", request.ProductId, request.CategoryId);
        try
        {
            await productCategoryService.RemoveProductFromCategoryAsync(request.ProductId, request.CategoryId, cancellationToken);
            logger.LogInformation("Product {ProductId} removed from category {CategoryId} successfully", request.ProductId, request.CategoryId);
            return ApiResponse<bool>.Factory.Success(true, "Product removed from category successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while removing product {ProductId} from category {CategoryId}", request.ProductId, request.CategoryId);
            throw;
        }
    }
}
