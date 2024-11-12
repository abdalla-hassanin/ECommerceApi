using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Queries;

public record GetProductImagesForProductQuery(string ProductId) : IRequest<ApiResponse<IReadOnlyList<ProductImageDto>>>;

public class GetProductImagesForProductHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<GetProductImagesForProductHandler> logger) : IRequestHandler<GetProductImagesForProductQuery, ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductImageDto>>> Handle(GetProductImagesForProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting images for product {ProductId}", request.ProductId);
        try
        {
            var images = await productImageService.GetImagesForProductAsync(request.ProductId, cancellationToken);
            logger.LogInformation("Retrieved {Count} images for product {ProductId}", images.Count, request.ProductId);
            return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(mapper.Map<IReadOnlyList<ProductImageDto>>(images));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting images for product {ProductId}", request.ProductId);
            throw;
        }
    }
}
