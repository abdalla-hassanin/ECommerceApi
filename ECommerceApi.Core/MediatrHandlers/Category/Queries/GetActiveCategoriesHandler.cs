using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Queries;

public record GetActiveCategoriesQuery : IRequest<ApiResponse<IReadOnlyList<CategoryDto>>>;

public class GetActiveCategoriesHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<GetActiveCategoriesHandler> logger) : IRequestHandler<GetActiveCategoriesQuery, ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CategoryDto>>> Handle(GetActiveCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting active categories");
        try
        {
            var categories = await categoryService.GetActiveCategoriesAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} active categories", categories.Count);
            return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(mapper.Map<IReadOnlyList<CategoryDto>>(categories));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting active categories");
            throw;
        }
    }
}
