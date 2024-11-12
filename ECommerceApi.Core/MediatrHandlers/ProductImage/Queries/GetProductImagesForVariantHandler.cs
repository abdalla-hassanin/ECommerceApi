using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Queries;

public record GetProductImagesForVariantQuery(string VariantId) : IRequest<ApiResponse<IReadOnlyList<ProductImageDto>>>;

public class GetProductImagesForVariantHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<GetProductImagesForVariantHandler> logger) : IRequestHandler<GetProductImagesForVariantQuery, ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductImageDto>>> Handle(GetProductImagesForVariantQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting images for variant {VariantId}", request.VariantId);
        try
        {
            var images = await productImageService.GetImagesForVariantAsync(request.VariantId, cancellationToken);
            logger.LogInformation("Retrieved {Count} images for variant {VariantId}", images.Count, request.VariantId);
            return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(mapper.Map<IReadOnlyList<ProductImageDto>>(images));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting images for variant {VariantId}", request.VariantId);
            throw;
        }
    }
}
