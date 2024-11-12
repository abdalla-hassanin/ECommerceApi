using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Wishlist;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetWishlistForCustomerResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<WishlistDto>>>
{
    public ApiResponse<IReadOnlyList<WishlistDto>> GetExamples()
    {
        var wishlistItems = new List<WishlistDto>
        {
            new WishlistDto(
                WishlistId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CreatedAt: DateTime.UtcNow.AddDays(-5)
            ),
            new WishlistDto(
                WishlistId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CreatedAt: DateTime.UtcNow.AddDays(-2)
            )
        };

        return ApiResponse<IReadOnlyList<WishlistDto>>.Factory.Success(wishlistItems);
    }
}

public class GetWishlistItemResponseExample : IExamplesProvider<ApiResponse<WishlistDto>>
{
    public ApiResponse<WishlistDto> GetExamples()
    {
        var wishlistItem = new WishlistDto(
            WishlistId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CreatedAt: DateTime.UtcNow.AddDays(-5)
        );

        return ApiResponse<WishlistDto>.Factory.Success(wishlistItem);
    }
}

public class AddToWishlistResponseExample : IExamplesProvider<ApiResponse<WishlistDto>>
{
    public ApiResponse<WishlistDto> GetExamples()
    {
        var wishlistItem = new WishlistDto(
            WishlistId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CreatedAt: DateTime.UtcNow
        );

        return ApiResponse<WishlistDto>.Factory.Created(wishlistItem, "Item added to wishlist successfully");
    }
}

public class RemoveFromWishlistResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Item removed from wishlist successfully");
    }
}

public class ClearWishlistResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Wishlist cleared successfully");
    }
}

public class BadRequestWishlistResponseExample : IExamplesProvider<ApiResponse<WishlistDto>>
{
    public ApiResponse<WishlistDto> GetExamples()
    {
        return ApiResponse<WishlistDto>.Factory.BadRequest(
            "Invalid wishlist data",
            new List<string> { "CustomerId must be greater than 0", "ProductId must be greater than 0" }
        );
    }
}

public class UnauthorizedWishlistResponseExample : IExamplesProvider<ApiResponse<WishlistDto>>
{
    public ApiResponse<WishlistDto> GetExamples()
    {
        return ApiResponse<WishlistDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundWishlistResponseExample : IExamplesProvider<ApiResponse<WishlistDto>>
{
    public ApiResponse<WishlistDto> GetExamples()
    {
        return ApiResponse<WishlistDto>.Factory.NotFound("Wishlist item not found");
    }
}