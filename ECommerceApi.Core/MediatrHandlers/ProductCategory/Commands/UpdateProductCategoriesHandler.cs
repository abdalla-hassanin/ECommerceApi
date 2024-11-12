using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory.Commands;

public record UpdateProductCategoriesCommand(string ProductId, IEnumerable<string> CategoryIds) : IRequest<ApiResponse<bool>>;

public class UpdateProductCategoriesCommandValidator : AbstractValidator<UpdateProductCategoriesCommand>
{
    public UpdateProductCategoriesCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ProductId must be a valid ULID.");

        RuleFor(x => x.CategoryIds).NotEmpty().WithMessage("CategoryIds must not be empty.");
        RuleForEach(x => x.CategoryIds)
            .NotEmpty()
            .Length(26)
            .WithMessage("Each CategoryId must be a valid ULID.");
    }
}

public class UpdateProductCategoriesHandler(
    IProductCategoryService productCategoryService,
    ILogger<UpdateProductCategoriesHandler> logger) : IRequestHandler<UpdateProductCategoriesCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(UpdateProductCategoriesCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating categories for product {ProductId}", request.ProductId);
        try
        {
            await productCategoryService.UpdateProductCategoriesAsync(request.ProductId, request.CategoryIds, cancellationToken);
            logger.LogInformation("Categories updated successfully for product {ProductId}", request.ProductId);
            return ApiResponse<bool>.Factory.Success(true, "Product categories updated successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating categories for product {ProductId}", request.ProductId);
            throw;
        }
    }
}
