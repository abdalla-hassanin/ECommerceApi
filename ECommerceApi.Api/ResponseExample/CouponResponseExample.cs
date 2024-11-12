using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Coupon;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;


public class GetCouponByIdResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        var couponDto = new CouponDto(
            CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Code: "SUMMER2023",
            Description: "Summer Sale Discount",
            DiscountType: "Percentage",
            DiscountValue: 15,
            MinimumPurchase: 100,
            UsageLimit: 1000,
            UsageCount: 50,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddMonths(3),
            IsActive: true,
            CreatedAt: DateTime.UtcNow.AddDays(-10),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<CouponDto>.Factory.Success(couponDto);
    }
}

public class GetAllCouponsResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CouponDto>>>
{
    public ApiResponse<IReadOnlyList<CouponDto>> GetExamples()
    {
        var coupons = new List<CouponDto>
        {
            new CouponDto(
                CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Code: "SUMMER2023",
                Description: "Summer Sale Discount",
                DiscountType: "Percentage",
                DiscountValue: 15,
                MinimumPurchase: 100,
                UsageLimit: 1000,
                UsageCount: 50,
                StartDate: DateTime.UtcNow,
                EndDate: DateTime.UtcNow.AddMonths(3),
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-10),
                UpdatedAt: DateTime.UtcNow
            ),
            new CouponDto(
                CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Code: "WELCOME10",
                Description: "New Customer Discount",
                DiscountType: "Fixed",
                DiscountValue: 10,
                MinimumPurchase: 50,
                UsageLimit: null,
                UsageCount: 200,
                StartDate: null,
                EndDate: null,
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-30),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<CouponDto>>.Factory.Success(coupons);
    }
}

public class GetActiveCouponsResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<CouponDto>>>
{
    public ApiResponse<IReadOnlyList<CouponDto>> GetExamples()
    {
        var coupons = new List<CouponDto>
        {
            new CouponDto(
                CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Code: "SUMMER2023",
                Description: "Summer Sale Discount",
                DiscountType: "Percentage",
                DiscountValue: 15,
                MinimumPurchase: 100,
                UsageLimit: 1000,
                UsageCount: 50,
                StartDate: DateTime.UtcNow,
                EndDate: DateTime.UtcNow.AddMonths(3),
                IsActive: true,
                CreatedAt: DateTime.UtcNow.AddDays(-10),
                UpdatedAt: DateTime.UtcNow
            )
        };

        return ApiResponse<IReadOnlyList<CouponDto>>.Factory.Success(coupons);
    }
}

public class GetCouponByCodeResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        var couponDto = new CouponDto(
            CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Code: "SUMMER2023",
            Description: "Summer Sale Discount",
            DiscountType: "Percentage",
            DiscountValue: 15,
            MinimumPurchase: 100,
            UsageLimit: 1000,
            UsageCount: 50,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddMonths(3),
            IsActive: true,
            CreatedAt: DateTime.UtcNow.AddDays(-10),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<CouponDto>.Factory.Success(couponDto);
    }
}

public class ValidCouponResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Coupon is valid");
    }
}

public class InvalidCouponResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.BadRequest("Coupon is invalid");
    }
}

public class CreatedCouponResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        var couponDto = new CouponDto(
            CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Code: "NEWCOUPON",
            Description: "New Coupon",
            DiscountType: "Percentage",
            DiscountValue: 20,
            MinimumPurchase: 150,
            UsageLimit: 500,
            UsageCount: 0,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddMonths(1),
            IsActive: true,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<CouponDto>.Factory.Created(couponDto, "Coupon created successfully");
    }
}

public class UpdateCouponResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        var couponDto = new CouponDto(
            CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Code: "SUMMER2023-UPDATED",
            Description: "Updated Summer Sale Discount",
            DiscountType: "Percentage",
            DiscountValue: 20,
            MinimumPurchase: 120,
            UsageLimit: 1200,
            UsageCount: 51,
            StartDate: DateTime.UtcNow,
            EndDate: DateTime.UtcNow.AddMonths(4),
            IsActive: true,
            CreatedAt: DateTime.UtcNow.AddDays(-10),
            UpdatedAt: DateTime.UtcNow
        );

        return ApiResponse<CouponDto>.Factory.Success(couponDto, "Coupon updated successfully");
    }
}

public class DeleteCouponResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Coupon deleted successfully");
    }
}

public class BadRequestCouponResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        return ApiResponse<CouponDto>.Factory.BadRequest(
            "Invalid coupon data",
            new List<string> 
            { 
                "Coupon code is required.",
                "Discount value must be greater than 0.",
                "Start date must be earlier than end date."
            }
        );
    }
}

public class UnauthorizedCouponResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        return ApiResponse<CouponDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundCouponResponseExample : IExamplesProvider<ApiResponse<CouponDto>>
{
    public ApiResponse<CouponDto> GetExamples()
    {
        return ApiResponse<CouponDto>.Factory.NotFound("Coupon not found");
    }
}