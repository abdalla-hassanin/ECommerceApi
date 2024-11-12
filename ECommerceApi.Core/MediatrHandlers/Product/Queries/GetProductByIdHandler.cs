using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Queries;

public record GetProductByIdQuery(string ProductId) : IRequest<ApiResponse<ProductDto>>;

public class GetProductByIdHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<GetProductByIdHandler> logger) : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductDto>>
{
    public async Task<ApiResponse<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting product with ID: {ProductId}", request.ProductId);
        try
        {
            var product = await productService.GetProductByIdAsync(request.ProductId, cancellationToken);
            if (product is null)
            {
                logger.LogWarning("Product with ID: {ProductId} not found", request.ProductId);
                return ApiResponse<ProductDto>.Factory.NotFound("Product not found");
            }
            logger.LogInformation("Product with ID: {ProductId} retrieved successfully", request.ProductId);
            return ApiResponse<ProductDto>.Factory.Success(mapper.Map<ProductDto>(product));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting product with ID: {ProductId}", request.ProductId);
            throw;
        }
    }
}
