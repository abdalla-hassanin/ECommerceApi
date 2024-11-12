using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory.Commands;

public record AddProductToCategoryCommand(string ProductId, string CategoryId) : IRequest<ApiResponse<bool>>;

public class AddProductToCategoryCommandValidator : AbstractValidator<AddProductToCategoryCommand>
{
    public AddProductToCategoryCommandValidator()
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

public class AddProductToCategoryHandler(
    IProductCategoryService productCategoryService,
    ILogger<AddProductToCategoryHandler> logger) : IRequestHandler<AddProductToCategoryCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(AddProductToCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding product {ProductId} to category {CategoryId}", request.ProductId, request.CategoryId);
        try
        {
            await productCategoryService.AddProductToCategoryAsync(request.ProductId, request.CategoryId, cancellationToken);
            logger.LogInformation("Product {ProductId} added to category {CategoryId} successfully", request.ProductId, request.CategoryId);
            return ApiResponse<bool>.Factory.Success(true, "Product added to category successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding product {ProductId} to category {CategoryId}", request.ProductId, request.CategoryId);
            throw;
        }
    }
}
