using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Queries;

public record GetAllCategoriesQuery : IRequest<ApiResponse<IReadOnlyList<CategoryDto>>>;

public class GetAllCategoriesHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<GetAllCategoriesHandler> logger) : IRequestHandler<GetAllCategoriesQuery, ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all categories");
        try
        {
            var categories = await categoryService.GetAllCategoriesAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} categories", categories.Count);
            return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(mapper.Map<IReadOnlyList<CategoryDto>>(categories));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all categories");
            throw;
        }
    }
}
