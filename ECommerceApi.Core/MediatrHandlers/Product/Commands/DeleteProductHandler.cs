using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Commands;

public record DeleteProductCommand(string ProductId) : IRequest<ApiResponse<bool>>;

public class DeleteProductHandler(
    IProductService productService,
    ILogger<DeleteProductHandler> logger) : IRequestHandler<DeleteProductCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product with ID: {ProductId}", request.ProductId);
        try
        {
            await productService.DeleteProductAsync(request.ProductId, cancellationToken);
            logger.LogInformation("Product with ID: {ProductId} deleted successfully", request.ProductId);
            return ApiResponse<bool>.Factory.Success(true, "Product deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting product with ID: {ProductId}", request.ProductId);
            throw;
        }
    }
}
