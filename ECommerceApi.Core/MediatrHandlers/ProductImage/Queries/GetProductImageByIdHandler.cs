using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Queries;

public record GetProductImageByIdQuery(string ImageId) : IRequest<ApiResponse<ProductImageDto>>;

public class GetProductImageByIdHandler(
    IProductImageService productImageService,
    IMapper mapper,
    ILogger<GetProductImageByIdHandler> logger) : IRequestHandler<GetProductImageByIdQuery, ApiResponse<ProductImageDto>>
{
    public async Task<ApiResponse<ProductImageDto>> Handle(GetProductImageByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product image with ID {ImageId}", request.ImageId);
        try
        {
            var image = await productImageService.GetImageByIdAsync(request.ImageId, cancellationToken);
            if (image is null)
            {
                logger.LogWarning("Product image with ID {ImageId} not found", request.ImageId);
                return ApiResponse<ProductImageDto>.Factory.NotFound("Product image not found");
            }
            logger.LogInformation("Product image with ID {ImageId} retrieved successfully", request.ImageId);
            return ApiResponse<ProductImageDto>.Factory.Success(mapper.Map<ProductImageDto>(image));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting product image with ID {ImageId}", request.ImageId);
            throw;
        }
    }
}
