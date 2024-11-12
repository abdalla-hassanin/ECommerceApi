using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant.Commands;

public record CreateProductVariantCommand(
    string ProductId,
    string Name,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    int InventoryQuantity,
    int LowStockThreshold,
    decimal? Weight,
    string? WeightUnit
) : IRequest<ApiResponse<ProductVariantDto>>;

public class CreateProductVariantCommandValidator : AbstractValidator<CreateProductVariantCommand>
{
    public CreateProductVariantCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ProductId must be a valid ULID.");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.InventoryQuantity).GreaterThanOrEqualTo(0).WithMessage("InventoryQuantity must be greater than or equal to 0.");
        RuleFor(x => x.LowStockThreshold).GreaterThanOrEqualTo(0).WithMessage("LowStockThreshold must be greater than or equal to 0.");
    }
}

public class CreateProductVariantHandler(
    IProductVariantService productVariantService,
    IMapper mapper,
    ILogger<CreateProductVariantHandler> logger) 
    : IRequestHandler<CreateProductVariantCommand, ApiResponse<ProductVariantDto>>
{
    public async Task<ApiResponse<ProductVariantDto>> Handle(CreateProductVariantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new product variant for product {ProductId}", request.ProductId);

        try
        {
            var variant = mapper.Map<Data.Entities.ProductVariant>(request);
            logger.LogDebug("Mapped CreateProductVariantCommand to ProductVariant entity");

            var createdVariant = await productVariantService.CreateVariantAsync(variant, cancellationToken);
            logger.LogInformation("Product variant created successfully for product {ProductId}", request.ProductId);

            var variantDto = mapper.Map<ProductVariantDto>(createdVariant);
            logger.LogDebug("Mapped created ProductVariant entity to ProductVariantDto");

            return ApiResponse<ProductVariantDto>.Factory.Created(variantDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating product variant for product {ProductId}", request.ProductId);
            throw;
        }
    }
}