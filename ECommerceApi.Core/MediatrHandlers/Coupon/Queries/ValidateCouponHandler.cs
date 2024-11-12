using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Queries;

public record ValidateCouponQuery(string Code, decimal PurchaseAmount) : IRequest<ApiResponse<bool>>;

public class ValidateCouponHandler(
    ICouponService couponService,
    ILogger<ValidateCouponHandler> logger) : IRequestHandler<ValidateCouponQuery, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ValidateCouponQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Validating coupon with code {CouponCode} for purchase amount {PurchaseAmount}", request.Code, request.PurchaseAmount);
        try
        {
            var isValid = await couponService.IsCouponValidAsync(request.Code, request.PurchaseAmount, cancellationToken);
            if (isValid)
            {
                logger.LogInformation("Coupon with code {CouponCode} is valid for purchase amount {PurchaseAmount}", request.Code, request.PurchaseAmount);
                return ApiResponse<bool>.Factory.Success(true, "Coupon is valid");
            }
            else
            {
                logger.LogWarning("Coupon with code {CouponCode} is invalid for purchase amount {PurchaseAmount}", request.Code, request.PurchaseAmount);
                return ApiResponse<bool>.Factory.BadRequest("Coupon is invalid");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while validating coupon with code {CouponCode} for purchase amount {PurchaseAmount}", request.Code, request.PurchaseAmount);
            throw;
        }
    }
}
