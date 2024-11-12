using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Category;
using Swashbuckle.AspNetCore.Filters;

public class GetCategoryByIdResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        var categoryDto = new CategoryDto
        (
            CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Electronics",
            Description: "Electronic devices and accessories",
            ImageUrl: "https://example.com/images/electronics.jpg",
            IsActive: true,
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow,
            ParentCategoryId: null
        );

        return ApiResponse<CategoryDto>.Factory.Success(categoryDto);
    }
}

public class GetAllCategoriesResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
    {
        var categories = new List<CategoryDto>
        {
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Electronics",
                Description: "Electronic devices and accessories",
                ImageUrl: "https://example.com/images/electronics.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: null
            ),
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Clothing",
                Description: "Apparel and fashion items",
                ImageUrl: "https://example.com/images/clothing.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-15),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: null
            )
        };

        return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(categories);
    }
}

public class GetCategoriesByParentIdResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
    {
        var categories = new List<CategoryDto>
        {
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Smartphones",
                Description: "Mobile phones and accessories",
                ImageUrl: "https://example.com/images/smartphones.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-20),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N"
            ),
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Laptops",
                Description: "Portable computers",
                ImageUrl: "https://example.com/images/laptops.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-18),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N"
            )
        };

        return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(categories);
    }
}

public class GetActiveCategoriesResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
    {
        var categories = new List<CategoryDto>
        {
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Electronics",
                Description: "Electronic devices and accessories",
                ImageUrl: "https://example.com/images/electronics.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: null
            ),
            new CategoryDto(
                CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Clothing",
                Description: "Apparel and fashion items",
                ImageUrl: "https://example.com/images/clothing.jpg",
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-15),
                UpdatedAt: DateTime.UtcNow,
                ParentCategoryId: null
            )
        };

        return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(categories);
    }
}

public class CreatedCategoryResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        var categoryDto = new CategoryDto
        (
            CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Books",
            Description: "Physical and digital books",
            ImageUrl: "https://example.com/images/books.jpg",
            IsActive: true,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow,
            ParentCategoryId: null
        );

        return ApiResponse<CategoryDto>.Factory.Created(categoryDto, "Category created successfully");
    }
}

public class UpdateCategoryResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        var categoryDto = new CategoryDto
        (
            CategoryId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Updated Electronics",
            Description: "Updated electronic devices and accessories",
            ImageUrl: "https://example.com/images/updated-electronics.jpg",
            IsActive: true,
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow,
            ParentCategoryId: null
        );

        return ApiResponse<CategoryDto>.Factory.Success(categoryDto, "Category updated successfully");
    }
}

public class DeleteCategoryResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Category deleted successfully");
    }
}

public class BadRequestCategoryResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        return ApiResponse<CategoryDto>.Factory.BadRequest(
            "Invalid category data",
            new List<string> { "Name is required", "Description must not exceed 500 characters" }
        );
    }
}

public class UnauthorizedCategoryResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        return ApiResponse<CategoryDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundCategoryResponseExample : IExamplesProvider<ApiResponse<CategoryDto>>
{
    public ApiResponse<CategoryDto> GetExamples()
    {
        return ApiResponse<CategoryDto>.Factory.NotFound("Category not found");
    }
}