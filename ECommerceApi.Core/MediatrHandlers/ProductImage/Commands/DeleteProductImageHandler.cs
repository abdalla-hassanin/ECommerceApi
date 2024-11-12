using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage.Commands;

public record DeleteProductImageCommand(string ImageId) : IRequest<ApiResponse<bool>>;

public class DeleteProductImageHandler(
    IProductImageService productImageService,
    ILogger<DeleteProductImageHandler> logger) : IRequestHandler<DeleteProductImageCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product image with ID {ImageId}", request.ImageId);
        try
        {
            await productImageService.DeleteImageAsync(request.ImageId, cancellationToken);
            logger.LogInformation("Product image with ID {ImageId} deleted successfully", request.ImageId);
            return ApiResponse<bool>.Factory.Success(true, "Product image deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting product image with ID {ImageId}", request.ImageId);
            throw;
        }
    }
}
