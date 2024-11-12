using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Queries;

public record GetAllProductImagesQuery : IRequest<ApiResponse<IReadOnlyList<ProductImageDto>>>;

public class GetAllProductImagesHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<GetAllProductImagesHandler> logger) : IRequestHandler<GetAllProductImagesQuery, ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductImageDto>>> Handle(GetAllProductImagesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all product images");
        try
        {
            var images = await productImageService.GetAllImagesAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} product images", images.Count);
            return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(mapper.Map<IReadOnlyList<ProductImageDto>>(images));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all product images");
            throw;
        }
    }
}
