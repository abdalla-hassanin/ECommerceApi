using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant.Commands;

public record UpdateProductVariantCommand(
    string VariantId,
    string Name,
    decimal Price,
    decimal? CompareAtPrice,
    decimal? CostPrice,
    int InventoryQuantity,
    int LowStockThreshold,
    decimal? Weight,
    string? WeightUnit
) : IRequest<ApiResponse<ProductVariantDto>>;

public class UpdateProductVariantCommandValidator : AbstractValidator<UpdateProductVariantCommand>
{
    public UpdateProductVariantCommandValidator()
    {
        RuleFor(x => x.VariantId)
            .NotEmpty()
            .Length(26)
            .WithMessage("VariantId must be a valid ULID.");
            
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0.");
        RuleFor(x => x.InventoryQuantity).GreaterThanOrEqualTo(0).WithMessage("InventoryQuantity must be greater than or equal to 0.");
        RuleFor(x => x.LowStockThreshold).GreaterThanOrEqualTo(0).WithMessage("LowStockThreshold must be greater than or equal to 0.");
    }
}

public class UpdateProductVariantHandler(
    IProductVariantService productVariantService,
    IMapper mapper,
    ILogger<UpdateProductVariantHandler> logger) 
    : IRequestHandler<UpdateProductVariantCommand, ApiResponse<ProductVariantDto>>
{
    public async Task<ApiResponse<ProductVariantDto>> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating product variant {VariantId}", request.VariantId);

        try
        {
            var existingVariant = await productVariantService.GetVariantByIdAsync(request.VariantId, cancellationToken);
            if (existingVariant is null)
            {
                logger.LogWarning("Product variant {VariantId} not found", request.VariantId);
                return ApiResponse<ProductVariantDto>.Factory.NotFound("Product variant not found");
            }

            mapper.Map(request, existingVariant);
            logger.LogDebug("Mapped UpdateProductVariantCommand to existing ProductVariant entity");

            var updatedVariant = await productVariantService.UpdateVariantAsync(existingVariant, cancellationToken);
            logger.LogInformation("Product variant {VariantId} updated successfully", request.VariantId);

            var variantDto = mapper.Map<ProductVariantDto>(updatedVariant);
            logger.LogDebug("Mapped updated ProductVariant entity to ProductVariantDto");

            return ApiResponse<ProductVariantDto>.Factory.Success(variantDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating product variant {VariantId}", request.VariantId);
            throw;
        }
    }
}