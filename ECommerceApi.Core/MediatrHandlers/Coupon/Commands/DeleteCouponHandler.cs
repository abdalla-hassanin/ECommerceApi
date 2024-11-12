using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Commands;

public record DeleteCouponCommand(string CouponId) : IRequest<ApiResponse<bool>>;

public class DeleteCouponHandler(
    ICouponService couponService,
    ILogger<DeleteCouponHandler> logger) : IRequestHandler<DeleteCouponCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCouponCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting coupon with ID {CouponId}", request.CouponId);
        try
        {
            await couponService.DeleteCouponAsync(request.CouponId, cancellationToken);
            logger.LogInformation("Coupon with ID {CouponId} deleted successfully", request.CouponId);
            return ApiResponse<bool>.Factory.Success(true, "Coupon deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting coupon with ID {CouponId}", request.CouponId);
            throw;
        }
    }
}
