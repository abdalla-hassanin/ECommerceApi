using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Commands;

public record UpdateProductCommand(
    string ProductId,
    string Name,
    string Description,
    string ShortDescription,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    string Status
) : IRequest<ApiResponse<ProductDto>>;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage("Product ID is required.")
            .Length(26).WithMessage("Product ID must be a valid ULID (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("Product ID must be a valid ULID format.");
            
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(255).WithMessage("Name must not exceed 255 characters.");
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.");
        RuleFor(x => x.ShortDescription)
            .NotEmpty().WithMessage("Short description is required.")
            .MaximumLength(500).WithMessage("Short description must not exceed 500 characters.");
        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.CompareAtPrice)
            .GreaterThan(0).When(x => x.CompareAtPrice.HasValue)
            .WithMessage("Compare at price must be greater than 0 when provided.");
        RuleFor(x => x.CostPrice)
            .GreaterThan(0).When(x => x.CostPrice.HasValue)
            .WithMessage("Cost price must be greater than 0 when provided.");
        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .MaximumLength(50).WithMessage("Status must not exceed 50 characters.");
    }
}

public class UpdateProductHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<UpdateProductHandler> logger) : IRequestHandler<UpdateProductCommand, ApiResponse<ProductDto>>
{
    public async Task<ApiResponse<ProductDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product with ID: {ProductId}", request.ProductId);
        try
        {
            var existingProduct = await productService.GetProductByIdAsync(request.ProductId, cancellationToken);
            if (existingProduct is null)
            {
                logger.LogWarning("Product with ID: {ProductId} not found", request.ProductId);
                return ApiResponse<ProductDto>.Factory.NotFound("Product not found");
            }

            mapper.Map(request, existingProduct);
            var updatedProduct = await productService.UpdateProductAsync(existingProduct, cancellationToken);
            logger.LogInformation("Product with ID: {ProductId} updated successfully", request.ProductId);
            return ApiResponse<ProductDto>.Factory.Success(mapper.Map<ProductDto>(updatedProduct));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating product with ID: {ProductId}", request.ProductId);
            throw;
        }
    }
}
