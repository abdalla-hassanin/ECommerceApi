using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Review;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class GetReviewByIdResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        var reviewDto = new ReviewDto(
            ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Rating: 4,
            Title: "Great product!",
            Content: "I really enjoyed this product. It met all my expectations.",
            Status: "Approved",
            CreatedAt: DateTime.UtcNow.AddDays(-7),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ReviewDto>.Factory.Success(reviewDto);
    }
}

public class GetReviewsForProductResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ReviewDto>>>
{
    public ApiResponse<IReadOnlyList<ReviewDto>> GetExamples()
    {
        var reviews = new List<ReviewDto>
        {
            new ReviewDto(
                ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Rating: 4,
                Title: "Great product!",
                Content: "I really enjoyed this product. It met all my expectations.",
                Status: "Approved",
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow
            ),
            new ReviewDto(
                ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Rating: 5,
                Title: "Excellent!",
                Content: "This product exceeded my expectations. Highly recommended!",
                Status: "Approved",
                CreatedAt: DateTime.UtcNow.AddDays(-3),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<ReviewDto>>.Factory.Success(reviews);
    }
}

public class GetReviewsByCustomerResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<ReviewDto>>>
{
    public ApiResponse<IReadOnlyList<ReviewDto>> GetExamples()
    {
        var reviews = new List<ReviewDto>
        {
            new ReviewDto(
                ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Rating: 4,
                Title: "Great product!",
                Content: "I really enjoyed this product. It met all my expectations.",
                Status: "Approved",
                CreatedAt: DateTime.UtcNow.AddDays(-7),
                UpdatedAt: DateTime.UtcNow
            ),
            new ReviewDto(
                ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Rating: 3,
                Title: "Good, but could be better",
                Content: "The product is good overall, but there's room for improvement.",
                Status: "Approved",
                CreatedAt: DateTime.UtcNow.AddDays(-1),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<ReviewDto>>.Factory.Success(reviews);
    }
}

public class CreatedReviewResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        var reviewDto = new ReviewDto(
            ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Rating: 5,
            Title: "Amazing product!",
            Content: "This is the best product I've ever used. Absolutely love it!",
            Status: "Pending",
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ReviewDto>.Factory.Created(reviewDto, "Review created successfully");
    }
}

public class UpdateReviewResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        var reviewDto = new ReviewDto(
            ReviewId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Rating: 5,
            Title: "Updated: Even better than I thought!",
            Content: "After using the product for a while, I've found even more features to love!",
            Status: "Approved",
            CreatedAt: DateTime.UtcNow.AddDays(-7),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<ReviewDto>.Factory.Success(reviewDto, "Review updated successfully");
    }
}

public class DeleteReviewResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Review deleted successfully");
    }
}

public class BadRequestReviewResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        return ApiResponse<ReviewDto>.Factory.BadRequest(
            "Invalid review data",
            new List<string> { "Rating must be between 1 and 5", "Title is required" }
        );
    }
}

public class UnauthorizedReviewResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        return ApiResponse<ReviewDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundReviewResponseExample : IExamplesProvider<ApiResponse<ReviewDto>>
{
    public ApiResponse<ReviewDto> GetExamples()
    {
        return ApiResponse<ReviewDto>.Factory.NotFound("Review not found");
    }
}