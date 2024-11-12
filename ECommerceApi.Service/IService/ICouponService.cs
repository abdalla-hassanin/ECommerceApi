using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface ICouponService
{
    Task<Coupon?> GetCouponByIdAsync(string couponId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Coupon>> GetAllCouponsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default);
    Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default);
    Task<bool> IsCouponValidAsync(string code, decimal purchaseAmount, CancellationToken cancellationToken = default);

    
    Task<Coupon> CreateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task<Coupon> UpdateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task DeleteCouponAsync(string couponId, CancellationToken cancellationToken = default);
}