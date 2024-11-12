using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant.Commands;

public record DeleteProductVariantCommand(string VariantId) : IRequest<ApiResponse<bool>>;

public class DeleteProductVariantHandler(
    IProductVariantService productVariantService,
    ILogger<DeleteProductVariantHandler> logger) 
    : IRequestHandler<DeleteProductVariantCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting product variant {VariantId}", request.VariantId);

        try
        {
            await productVariantService.DeleteVariantAsync(request.VariantId, cancellationToken);
            logger.LogInformation("Product variant {VariantId} deleted successfully", request.VariantId);
            return ApiResponse<bool>.Factory.Success(true, "Product variant deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting product variant {VariantId}", request.VariantId);
            throw;
        }
    }
}