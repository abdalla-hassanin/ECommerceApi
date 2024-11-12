using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Commands;

public record DeleteCategoryCommand(string CategoryId) : IRequest<ApiResponse<bool>>;

public class DeleteCategoryHandler(
    ICategoryService categoryService,
    ILogger<DeleteCategoryHandler> logger) : IRequestHandler<DeleteCategoryCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting category with ID {CategoryId}", request.CategoryId);
        try
        {
            await categoryService.DeleteCategoryAsync(request.CategoryId, cancellationToken);
            logger.LogInformation("Category with ID {CategoryId} deleted successfully", request.CategoryId);
            return ApiResponse<bool>.Factory.Success(true, "Category deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting category with ID {CategoryId}", request.CategoryId);
            throw;
        }
    }
}
