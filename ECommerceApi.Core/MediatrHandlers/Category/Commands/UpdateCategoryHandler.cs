using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Commands;

public record UpdateCategoryCommand(
    string CategoryId,
    string Name,
    string Description,
    IFormFile? ImageFile,
    bool IsActive,
    string? ParentCategoryId
) : IRequest<ApiResponse<CategoryDto>>;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(x => x.CategoryId).NotEmpty().Length(26).WithMessage("Valid ULID is required.");
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Description is required and must not exceed 500 characters.");
        RuleFor(x => x.ParentCategoryId).Length(26).When(x => x.ParentCategoryId != null).WithMessage("Parent category ID must be a valid ULID.");
    }
}

public class UpdateCategoryHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<UpdateCategoryHandler> logger) : IRequestHandler<UpdateCategoryCommand, ApiResponse<CategoryDto>>
{
    public async Task<ApiResponse<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating category with ID {CategoryId}", request.CategoryId);
        try
        {
            var existingCategory = await categoryService.GetCategoryByIdAsync(request.CategoryId, cancellationToken);
            if (existingCategory is null)
            {
                logger.LogWarning("Category with ID {CategoryId} not found", request.CategoryId);
                return ApiResponse<CategoryDto>.Factory.NotFound("Category not found");
            }

            mapper.Map(request, existingCategory);
            var updatedCategory = await categoryService.UpdateCategoryAsync(existingCategory,request.ImageFile, cancellationToken);
            logger.LogInformation("Category with ID {CategoryId} updated successfully", request.CategoryId);
            return ApiResponse<CategoryDto>.Factory.Success(mapper.Map<CategoryDto>(updatedCategory));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating category with ID {CategoryId}", request.CategoryId);
            throw;
        }
    }
}
