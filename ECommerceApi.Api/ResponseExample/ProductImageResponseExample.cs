using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.ProductImage;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetProductImageByIdResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        var productImageDto = new ProductImageDto(
            ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ImageUrl: "https://example.com/image1.jpg",
            AltText: "Product Image 1",
            DisplayOrder: 1,
            CreatedAt: DateTime.UtcNow.AddDays(-7),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductImageDto>.Factory.Success(productImageDto);
    }
}

public class GetAllProductImagesResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public ApiResponse<IReadOnlyList<ProductImageDto>> GetExamples()
    {
        var productImages = new List<ProductImageDto>
        {
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ImageUrl: "https://example.com/image1.jpg",
                AltText: "Product Image 1",
                DisplayOrder: 1,
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow
            ),
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: null,
                ImageUrl: "https://example.com/image2.jpg",
                AltText: "Product Image 2",
                DisplayOrder: 1,
                CreatedAt: DateTime.UtcNow.AddDays(-6),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(productImages);
    }
}

public class GetProductImagesForProductResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public ApiResponse<IReadOnlyList<ProductImageDto>> GetExamples()
    {
        var productImages = new List<ProductImageDto>
        {
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ImageUrl: "https://example.com/image1.jpg",
                AltText: "Product Image 1",
                DisplayOrder: 1,
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow
            ),
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ImageUrl: "https://example.com/image2.jpg",
                AltText: "Product Image 2",
                DisplayOrder: 2,
                CreatedAt: DateTime.UtcNow.AddDays(-6),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(productImages);
    }
}

public class GetProductImagesForVariantResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ProductImageDto>>>
{
    public ApiResponse<IReadOnlyList<ProductImageDto>> GetExamples()
    {
        var productImages = new List<ProductImageDto>
        {
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ImageUrl: "https://example.com/image1.jpg",
                AltText: "Variant Image 1",
                DisplayOrder: 1,
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow
            ),
            new ProductImageDto(
                ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ImageUrl: "https://example.com/image2.jpg",
                AltText: "Variant Image 2",
                DisplayOrder: 2,
                CreatedAt: DateTime.UtcNow.AddDays(-6),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<ProductImageDto>>.Factory.Success(productImages);
    }
}

public class CreatedProductImageResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        var productImageDto = new ProductImageDto(
            ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            VariantId: null,
            ImageUrl: "https://example.com/newimage.jpg",
            AltText: "New Product Image",
            DisplayOrder: 1,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductImageDto>.Factory.Created(productImageDto, "Product image created successfully");
    }
}

public class UpdateProductImageResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        var productImageDto = new ProductImageDto(
            ImageId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            VariantId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ImageUrl: "https://example.com/updatedimage.jpg",
            AltText: "Updated Product Image",
            DisplayOrder: 2,
            CreatedAt: DateTime.UtcNow.AddDays(-7),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ProductImageDto>.Factory.Success(productImageDto, "Product image updated successfully");
    }
}

public class DeleteProductImageResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Product image deleted successfully");
    }
}

public class BadRequestProductImageResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        return ApiResponse<ProductImageDto>.Factory.BadRequest(
            "Invalid product image data",
            new List<string> { "ImageUrl is required", "DisplayOrder must be greater than or equal to 0" }
        );
    }
}

public class UnauthorizedProductImageResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        return ApiResponse<ProductImageDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundProductImageResponseExample : IExamplesProvider<ApiResponse<ProductImageDto>>
{
    public ApiResponse<ProductImageDto> GetExamples()
    {
        return ApiResponse<ProductImageDto>.Factory.NotFound("Product image not found");
    }
}