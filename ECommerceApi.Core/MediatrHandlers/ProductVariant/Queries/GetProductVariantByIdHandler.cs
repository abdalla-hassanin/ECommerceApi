using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant.Queries;

public record GetProductVariantByIdQuery(string VariantId) : IRequest<ApiResponse<ProductVariantDto>>;

public class GetProductVariantByIdHandler(
    IProductVariantService productVariantService,
    IMapper mapper,
    ILogger<GetProductVariantByIdHandler> logger) 
    : IRequestHandler<GetProductVariantByIdQuery, ApiResponse<ProductVariantDto>>
{
    public async Task<ApiResponse<ProductVariantDto>> Handle(GetProductVariantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product variant {VariantId}", request.VariantId);

        try
        {
            var variant = await productVariantService.GetVariantByIdAsync(request.VariantId, cancellationToken);
            if (variant is null)
            {
                logger.LogWarning("Product variant {VariantId} not found", request.VariantId);
                return ApiResponse<ProductVariantDto>.Factory.NotFound("Product variant not found");
            }

            var variantDto = mapper.Map<ProductVariantDto>(variant);
            logger.LogDebug("Mapped ProductVariant entity to ProductVariantDto");

            return ApiResponse<ProductVariantDto>.Factory.Success(variantDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting product variant {VariantId}", request.VariantId);
            throw;
        }
    }
}