using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Product;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetProductByIdResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        var productDto = new ProductDto(
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Sample Product",
            Description: "This is a sample product description.",
            ShortDescription: "Sample product",
            Price: 99.99m,
            CompareAtPrice: 129.99m,
            CostPrice: 75.00m,
            Status: "Active",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductDto>.Factory.Success(productDto);
    }
}

public class GetAllProductsResponseExample : IExamplesProvider<ApiResponse<IEnumerable<ProductDto>>>
{
    public ApiResponse<IEnumerable<ProductDto>> GetExamples()
    {
        var products = new List<ProductDto>
        {
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Product 1", "Description 1", "Short 1", 99.99m, 129.99m, 75.00m, "Active", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow),
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Product 2", "Description 2", "Short 2", 149.99m, 199.99m, 100.00m, "Active", DateTime.UtcNow.AddDays(-25), DateTime.UtcNow)
        };

        return ApiResponse<IEnumerable<ProductDto>>.Factory.Success(products);
    }
}

public class SearchProductsResponseExample : IExamplesProvider<ApiResponse<IEnumerable<ProductDto>>>
{
    public ApiResponse<IEnumerable<ProductDto>> GetExamples()
    {
        var products = new List<ProductDto>
        {
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Search Result 1", "Description 1", "Short 1", 99.99m, 129.99m, 75.00m, "Active", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow),
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Search Result 2", "Description 2", "Short 2", 149.99m, 199.99m, 100.00m, "Active", DateTime.UtcNow.AddDays(-25), DateTime.UtcNow)
        };

        var pagination = new PaginationMetadata
        {
            CurrentPage = 1,
            TotalPages = 5,
            PageSize = 10,
            TotalCount = 50
        };

        return ApiResponse<IEnumerable<ProductDto>>.Factory.WithPagination(products, pagination, "Search results retrieved successfully");
    }
}

public class GetProductsByCategoryResponseExample : IExamplesProvider<ApiResponse<IEnumerable<ProductDto>>>
{
    public ApiResponse<IEnumerable<ProductDto>> GetExamples()
    {
        var products = new List<ProductDto>
        {
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Category Product 1", "Description 1", "Short 1", 99.99m, 129.99m, 75.00m, "Active", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow),
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Category Product 2", "Description 2", "Short 2", 149.99m, 199.99m, 100.00m, "Active", DateTime.UtcNow.AddDays(-25), DateTime.UtcNow)
        };

        var pagination = new PaginationMetadata
        {
            CurrentPage = 1,
            TotalPages = 3,
            PageSize = 10,
            TotalCount = 25
        };

        return ApiResponse<IEnumerable<ProductDto>>.Factory.WithPagination(products, pagination, "Products in category retrieved successfully");
    }
}

public class GetActiveProductsResponseExample : IExamplesProvider<ApiResponse<IEnumerable<ProductDto>>>
{
    public ApiResponse<IEnumerable<ProductDto>> GetExamples()
    {
        var products = new List<ProductDto>
        {
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Active Product 1", "Description 1", "Short 1", 99.99m, 129.99m, 75.00m, "Active", DateTime.UtcNow.AddDays(-30), DateTime.UtcNow),
            new ProductDto("01HF3WFKX1KPY89WNJRXJ6V18N", "Active Product 2", "Description 2", "Short 2", 149.99m, 199.99m, 100.00m, "Active", DateTime.UtcNow.AddDays(-25), DateTime.UtcNow)
        };

        var pagination = new PaginationMetadata
        {
            CurrentPage = 1,
            TotalPages = 4,
            PageSize = 10,
            TotalCount = 35
        };

        return ApiResponse<IEnumerable<ProductDto>>.Factory.WithPagination(products, pagination, "Active products retrieved successfully");
    }
}

public class CreatedProductResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        var productDto = new ProductDto(
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "New Product",
            Description: "This is a newly created product.",
            ShortDescription: "New product",
            Price: 79.99m,
            CompareAtPrice: 99.99m,
            CostPrice: 50.00m,
            Status: "Active",
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductDto>.Factory.Created(productDto, "Product created successfully");
    }
}

public class UpdateProductResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        var productDto = new ProductDto(
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Updated Product",
            Description: "This product has been updated.",
            ShortDescription: "Updated product",
            Price: 89.99m,
            CompareAtPrice: 119.99m,
            CostPrice: 65.00m,
            Status: "Active",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductDto>.Factory.Success(productDto, "Product updated successfully");
    }
}

public class DeleteProductResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product deleted successfully");
    }
}

public class BadRequestProductResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        return ApiResponse<ProductDto>.Factory.BadRequest(
            "Invalid product data",
            new List<string> { "Name is required", "Price must be greater than 0" }
        );
    }
}

public class UnauthorizedProductResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        return ApiResponse<ProductDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundProductResponseExample : IExamplesProvider<ApiResponse<ProductDto>>
{
    public ApiResponse<ProductDto> GetExamples()
    {
        return ApiResponse<ProductDto>.Factory.NotFound("Product not found");
    }
}