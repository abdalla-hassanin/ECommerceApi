using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Category.Commands;

public record CreateCategoryCommand(
    string Name,
    string Description,
    IFormFile ImageFile,
    bool IsActive,
    string? ParentCategoryId
) : IRequest<ApiResponse<CategoryDto>>;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100).WithMessage("Name is required and must not exceed 100 characters.");
        RuleFor(x => x.Description).NotEmpty().MaximumLength(500).WithMessage("Description is required and must not exceed 500 characters.");
        RuleFor(x => x.ImageFile).NotNull().WithMessage("ImageFile is required.");
        RuleFor(x => x.ParentCategoryId).Length(26).When(x => x.ParentCategoryId != null).WithMessage("Parent category ID must be a valid ULID.");
    }
}

public class CreateCategoryHandler(
    ICategoryService categoryService,
    IMapper mapper,
    ILogger<CreateCategoryHandler> logger) : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryDto>>
{
    public async Task<ApiResponse<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new category");
        try
        {
            var category = mapper.Map<Data.Entities.Category>(request);
            var createdCategory = await categoryService.CreateCategoryAsync(category,request.ImageFile, cancellationToken);
            logger.LogInformation("Category created successfully with ID {CategoryId}", createdCategory.CategoryId);
            return ApiResponse<CategoryDto>.Factory.Created(mapper.Map<CategoryDto>(createdCategory));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating category");
            throw;
        }
    }
}
