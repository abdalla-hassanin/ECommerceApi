namespace ECommerceApi.Core.MediatrHandlers.Coupon;

public record CouponDto(
    string CouponId,
    string Code,
    string Description,
    string DiscountType,
    decimal DiscountValue,
    decimal? MinimumPurchase,
    int? UsageLimit,
    int UsageCount,
    DateTime? StartDate,
    DateTime? EndDate,
    bool IsActive,
    DateTime CreatedAt,
    DateTime UpdatedAt
);