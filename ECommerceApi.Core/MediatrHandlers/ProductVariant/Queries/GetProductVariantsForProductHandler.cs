using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant.Queries;

public record GetProductVariantsForProductQuery(string ProductId) : IRequest<ApiResponse<IReadOnlyList<ProductVariantDto>>>;

public class GetProductVariantsForProductHandler(
    IProductVariantService productVariantService,
    IMapper mapper,
    ILogger<GetProductVariantsForProductHandler> logger) 
    : IRequestHandler<GetProductVariantsForProductQuery, ApiResponse<IReadOnlyList<ProductVariantDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductVariantDto>>> Handle(GetProductVariantsForProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product variants for product {ProductId}", request.ProductId);

        try
        {
            var variants = await productVariantService.GetVariantsForProductAsync(request.ProductId, cancellationToken);
            logger.LogDebug("Retrieved {Count} variants for product {ProductId}", variants.Count, request.ProductId);

            var variantDtos = mapper.Map<IReadOnlyList<ProductVariantDto>>(variants);
            logger.LogDebug("Mapped ProductVariant entities to ProductVariantDto list");

            return ApiResponse<IReadOnlyList<ProductVariantDto>>.Factory.Success(variantDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting product variants for product {ProductId}", request.ProductId);
            throw;
        }
    }
}