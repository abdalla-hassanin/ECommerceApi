using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Category;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory.Queries;

public record GetCategoriesForProductQuery(string ProductId) : IRequest<ApiResponse<IReadOnlyList<CategoryDto>>>;

public class GetCategoriesForProductHandler(
    IProductCategoryService productCategoryService,
    IMapper mapper,
    ILogger<GetCategoriesForProductHandler> logger) : IRequestHandler<GetCategoriesForProductQuery, ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CategoryDto>>> Handle(GetCategoriesForProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting categories for product {ProductId}", request.ProductId);
        try
        {
            var categories = await productCategoryService.GetCategoriesForProductAsync(request.ProductId, cancellationToken);
            logger.LogInformation("Retrieved {Count} categories for product {ProductId}", categories.Count, request.ProductId);
            return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(mapper.Map<IReadOnlyList<CategoryDto>>(categories));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting categories for product {ProductId}", request.ProductId);
            throw;
        }
    }
}
