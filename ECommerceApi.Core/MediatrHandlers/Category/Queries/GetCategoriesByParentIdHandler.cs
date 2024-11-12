using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Queries;

public record GetCategoriesByParentIdQuery(string ParentCategoryId) : IRequest<ApiResponse<IReadOnlyList<CategoryDto>>>;

public class GetCategoriesByParentIdHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<GetCategoriesByParentIdHandler> logger) : IRequestHandler<GetCategoriesByParentIdQuery, ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CategoryDto>>> Handle(GetCategoriesByParentIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting categories by parent ID {ParentCategoryId}", request.ParentCategoryId);
        try
        {
            var categories = await categoryService.GetCategoriesByParentIdAsync(request.ParentCategoryId, cancellationToken);
            logger.LogInformation("Retrieved {Count} categories for parent ID {ParentCategoryId}", categories.Count, request.ParentCategoryId);
            return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(mapper.Map<IReadOnlyList<CategoryDto>>(categories));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting categories for parent ID {ParentCategoryId}", request.ParentCategoryId);
            throw;
        }
    }
}
