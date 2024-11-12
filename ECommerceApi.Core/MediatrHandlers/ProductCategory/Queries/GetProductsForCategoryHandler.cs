using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Product;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory.Queries;

public record GetProductsForCategoryQuery(string CategoryId) : IRequest<ApiResponse<IReadOnlyList<ProductDto>>>;

public class GetProductsForCategoryHandler(
    IProductCategoryService productCategoryService,
    IMapper mapper,
    ILogger<GetProductsForCategoryHandler> logger) : IRequestHandler<GetProductsForCategoryQuery, ApiResponse<IReadOnlyList<ProductDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ProductDto>>> Handle(GetProductsForCategoryQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting products for category {CategoryId}", request.CategoryId);
        try
        {
            var products = await productCategoryService.GetProductsForCategoryAsync(request.CategoryId, cancellationToken);
            logger.LogInformation("Retrieved {Count} products for category {CategoryId}", products.Count, request.CategoryId);
            return ApiResponse<IReadOnlyList<ProductDto>>.Factory.Success(mapper.Map<IReadOnlyList<ProductDto>>(products));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting products for category {CategoryId}", request.CategoryId);
            throw;
        }
    }
}
