using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Product.Queries;

public record GetAllProductsQuery : IRequest<ApiResponse<IReadOnlyList<ProductDto>>>;

public class GetAllProductsHandler(
    IProductService productService,
    IMapper mapper,
    ILogger<GetAllProductsHandler> logger) : IRequestHandler<GetAllProductsQuery, ApiResponse<IReadOnlyList<ProductDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all products");
        try
        {
            var products = await productService.GetAllProductsAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} products", products.Count);
            return ApiResponse<IReadOnlyList<ProductDto>>.Factory.Success(mapper.Map<IReadOnlyList<ProductDto>>(products));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all products");
            throw;
        }
    }
}
