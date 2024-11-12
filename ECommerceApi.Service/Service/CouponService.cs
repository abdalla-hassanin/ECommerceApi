using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class CouponService : ICouponService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CouponService> _logger;

    public CouponService(IUnitOfWork unitOfWork, ILogger<CouponService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Coupon?> GetCouponByIdAsync(string couponId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting coupon with ID: {CouponId}", couponId);
        return await _unitOfWork.Repository<Coupon>().GetByIdAsync(couponId, cancellationToken);
    }

    public async Task<IReadOnlyList<Coupon>> GetAllCouponsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all coupons");
        return await _unitOfWork.Repository<Coupon>().ListAllAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting active coupons");
        return await _unitOfWork.Repository<Coupon>()
            .ListAsync(new CouponSpecifications.ActiveOnly(), cancellationToken);
    }

    public async Task<Coupon?> GetCouponByCodeAsync(string code, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting coupon by code: {CouponCode}", code);
        var spec = new CouponSpecifications.ByCode(code);
        var coupons = await _unitOfWork.Repository<Coupon>().ListAsync(spec, cancellationToken);
        return coupons.FirstOrDefault();
    }

    public async Task<bool> IsCouponValidAsync(string code, decimal purchaseAmount, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Validating coupon: {CouponCode} for purchase amount: {PurchaseAmount}", code, purchaseAmount);
        var coupon = await GetCouponByCodeAsync(code, cancellationToken);
        if (coupon == null || !coupon.IsActive)
        {
            _logger.LogInformation("Coupon {CouponCode} is invalid or inactive", code);
            return false;
        }

        if (coupon.EndDate.HasValue && coupon.EndDate < DateTime.UtcNow)
        {
            _logger.LogInformation("Coupon {CouponCode} has expired", code);
            return false;
        }

        if (coupon.StartDate.HasValue && coupon.StartDate > DateTime.UtcNow)
        {
            _logger.LogInformation("Coupon {CouponCode} is not yet active", code);
            return false;
        }

        if (coupon.MinimumPurchase.HasValue && purchaseAmount < coupon.MinimumPurchase.Value)
        {
            _logger.LogInformation("Purchase amount does not meet minimum requirement for coupon {CouponCode}", code);
            return false;
        }

        if (coupon.UsageLimit.HasValue && coupon.UsageCount >= coupon.UsageLimit.Value)
        {
            _logger.LogInformation("Coupon {CouponCode} has reached its usage limit", code);
            return false;
        }

        _logger.LogInformation("Coupon {CouponCode} is valid", code);
        return true;
    }

    public async Task<Coupon> CreateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new coupon: {CouponCode}", coupon.Code);
        await _unitOfWork.Repository<Coupon>().AddAsync(coupon, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Coupon created with ID: {CouponId}", coupon.CouponId);
        return coupon;
    }

    public async Task<Coupon> UpdateCouponAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating coupon with ID: {CouponId}", coupon.CouponId);
        await _unitOfWork.Repository<Coupon>().UpdateAsync(coupon, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Coupon updated successfully");
        return coupon;
    }

    public async Task DeleteCouponAsync(string couponId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting coupon with ID: {CouponId}", couponId);
        var coupon = await GetCouponByIdAsync(couponId, cancellationToken);
        if (coupon is not null)
        {
            await _unitOfWork.Repository<Coupon>().DeleteAsync(coupon, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Coupon deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent coupon with ID: {CouponId}", couponId);
        }
    }
}