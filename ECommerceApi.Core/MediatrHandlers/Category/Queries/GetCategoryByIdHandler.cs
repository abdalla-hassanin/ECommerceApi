using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Queries;

public record GetCategoryByIdQuery(string CategoryId) : IRequest<ApiResponse<CategoryDto>>;

public class GetCategoryByIdHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<GetCategoryByIdHandler> logger) : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryDto>>
{
    public async Task<ApiResponse<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting category with ID {CategoryId}", request.CategoryId);
        try
        {
            var category = await categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (category is null)
            {
                logger.LogWarning("Category with ID {CategoryId} not found", request.CategoryId);
                return ApiResponse<CategoryDto>.Factory.NotFound("Category not found");
            }
            logger.LogInformation("Retrieved category with ID {CategoryId}", request.CategoryId);
            return ApiResponse<CategoryDto>.Factory.Success(mapper.Map<CategoryDto>(category));
        }     
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting category with ID {CategoryId}", request.CategoryId);
            throw;
        } 
    }
}


