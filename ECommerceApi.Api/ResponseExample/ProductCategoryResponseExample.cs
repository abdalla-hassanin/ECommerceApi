using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Category;
using ECommerceApi.Core.MediatrHandlers.Product;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetCategoriesForProductResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CategoryDto>>>
{
    public ApiResponse<IReadOnlyList<CategoryDto>> GetExamples()
    {
        var categories = new List<CategoryDto>
        {
            new CategoryDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Electronics", "Electronic devices", "https://example.com/electronics.jpg", true,
                DateTime.UtcNow.AddDays(-30), DateTime.UtcNow, null),
            new CategoryDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Smartphones", "Mobile phones", "https://example.com/smartphones.jpg", true,
                DateTime.UtcNow.AddDays(-30), DateTime.UtcNow, "01HF3WFKX1KPY89WNJRXJ6V18N")
        };

        return ApiResponse<IReadOnlyList<CategoryDto>>.Factory.Success(categories);
    }
}

public class GetProductsForCategoryResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ProductDto>>>
{
    public ApiResponse<IReadOnlyList<ProductDto>> GetExamples()
    {
        var products = new List<ProductDto>
        {
            new ProductDto
            (
                ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name : "iPhone 12",
                Description : "The latest iPhone model with 5G capability and A14 Bionic chip.",
                ShortDescription : "5G Smartphone",
                Price : 999.99m,
                CompareAtPrice : 1099.99m,
                CostPrice : 800m,
                Status : "Active",
                CreatedAt : DateTime.UtcNow.AddDays(-30),
                UpdatedAt : DateTime.UtcNow
            ),
            new ProductDto
            (
                ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name : "Samsung Galaxy S21",
                Description : "Samsung's flagship smartphone with advanced camera features and 5G support.",
                ShortDescription : "5G Android Smartphone",
                Price : 899.99m,
                CompareAtPrice : 999.99m,
                CostPrice : 700m,
                Status : "Active",
                CreatedAt : DateTime.UtcNow.AddDays(-25),
                UpdatedAt : DateTime.UtcNow.AddDays(-1)
            )
        };


        return ApiResponse<IReadOnlyList<ProductDto>>.Factory.Success(products);
    }
}

public class AddProductToCategoryResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product added to category successfully");
    }
}

public class RemoveProductFromCategoryResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product removed from category successfully");
    }
}

public class UpdateProductCategoriesResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product categories updated successfully");
    }
}

public class BadRequestProductCategoryResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.BadRequest(
            "Invalid product category data",
            new List<string> { "ProductId must be greater than 0", "CategoryId must be greater than 0" }
        );
    }
}

public class UnauthorizedProductCategoryResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Unauthorized("Unauthorized access");
    }
}