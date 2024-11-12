using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.ProductImage;
using ECommerceApi.Core.MediatrHandlers.ProductVariant;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetProductVariantByIdResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        var productVariantDto = new ProductVariantDto(
            VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Large / Red",
            Price: 29.99m,
            CompareAtPrice: 39.99m,
            CostPrice: 15.00m,
            InventoryQuantity: 100,
            LowStockThreshold: 10,
            Weight: 0.5m,
            WeightUnit: "kg",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow,
            Images: new List<ProductImageDto>
            {
                new ProductImageDto
                (
                    ImageId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ImageUrl : "https://example.com/image1.jpg",
                    AltText : "Large Red Variant",
                    DisplayOrder : 1,
                    CreatedAt : DateTime.UtcNow.AddDays(-30),
                    UpdatedAt : DateTime.UtcNow
                )
            }
        );

        return ApiResponse<ProductVariantDto>.Factory.Success(productVariantDto);
    }
}

public class
    GetProductVariantsForProductResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ProductVariantDto>>>
{
    public ApiResponse<IReadOnlyList<ProductVariantDto>> GetExamples()
    {
        var productVariants = new List<ProductVariantDto>
        {
            new ProductVariantDto(
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Large / Red",
                Price: 29.99m,
                CompareAtPrice: 39.99m,
                CostPrice: 15.00m,
                InventoryQuantity: 100,
                LowStockThreshold: 10,
                Weight: 0.5m,
                WeightUnit: "kg",
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow,
                Images: new List<ProductImageDto>
                {
                    new ProductImageDto
                    (ImageId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        VariantId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        ImageUrl : "https://example.com/image1.jpg",
                        AltText : "Large Red Variant",
                        DisplayOrder : 1,
                        CreatedAt : DateTime.UtcNow.AddDays(-30),
                        UpdatedAt : DateTime.UtcNow
                    )
                }
            ),
            new ProductVariantDto(
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Name: "Medium / Blue",
                Price: 27.99m,
                CompareAtPrice: 37.99m,
                CostPrice: 14.00m,
                InventoryQuantity: 75,
                LowStockThreshold: 10,
                Weight: 0.45m,
                WeightUnit: "kg",
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow,
                Images: new List<ProductImageDto>
                {
                    new ProductImageDto
                    (
                        ImageId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        VariantId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                        ImageUrl : "https://example.com/image2.jpg",
                        AltText : "Medium Blue Variant",
                        DisplayOrder : 1,
                        CreatedAt : DateTime.UtcNow.AddDays(-30),
                        UpdatedAt : DateTime.UtcNow
                    )
                }
            )
        };

        return ApiResponse<IReadOnlyList<ProductVariantDto>>.Factory.Success(productVariants);
    }
}

public class CreatedProductVariantResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        var productVariantDto = new ProductVariantDto(
            VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Small / Green",
            Price: 25.99m,
            CompareAtPrice: 35.99m,
            CostPrice: 13.00m,
            InventoryQuantity: 50,
            LowStockThreshold: 10,
            Weight: 0.4m,
            WeightUnit: "kg",
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow,
            Images: new List<ProductImageDto>()
        );

        return ApiResponse<ProductVariantDto>.Factory.Created(productVariantDto,
            "Product variant created successfully");
    }
}

public class UpdateProductVariantResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        var productVariantDto = new ProductVariantDto(
            VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Name: "Large / Red (Updated)",
            Price: 32.99m,
            CompareAtPrice: 42.99m,
            CostPrice: 16.00m,
            InventoryQuantity: 120,
            LowStockThreshold: 15,
            Weight: 0.55m,
            WeightUnit: "kg",
            CreatedAt: DateTime.UtcNow.AddDays(-30),
            UpdatedAt: DateTime.UtcNow,
            Images: new List<ProductImageDto>
            {
                new ProductImageDto
                (
                    ImageId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId : "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ImageUrl : "https://example.com/image1_updated.jpg",
                    AltText : "Updated Large Red Variant",
                    DisplayOrder : 1,
                    CreatedAt : DateTime.UtcNow.AddDays(-30),
                    UpdatedAt : DateTime.UtcNow
                )
            }
        );

        return ApiResponse<ProductVariantDto>.Factory.Success(productVariantDto,
            "Product variant updated successfully");
    }
}

public class DeleteProductVariantResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product variant deleted successfully");
    }
}

public class BadRequestProductVariantResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        return ApiResponse<ProductVariantDto>.Factory.BadRequest(
            "Invalid product variant data",
            new List<string> { "Name is required", "Price must be greater than 0" }
        );
    }
}

public class UnauthorizedProductVariantResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        return ApiResponse<ProductVariantDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundProductVariantResponseExample : IExamplesProvider<ApiResponse<ProductVariantDto>>
{
    public ApiResponse<ProductVariantDto> GetExamples()
    {
        return ApiResponse<ProductVariantDto>.Factory.NotFound("Product variant not found");
    }
}