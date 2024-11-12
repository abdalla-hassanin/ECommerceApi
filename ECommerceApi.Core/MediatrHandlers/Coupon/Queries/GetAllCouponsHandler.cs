using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Queries;

public record GetAllCouponsQuery : IRequest<ApiResponse<IReadOnlyList<CouponDto>>>;

public class GetAllCouponsHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<GetAllCouponsHandler> logger) : IRequestHandler<GetAllCouponsQuery, ApiResponse<IReadOnlyList<CouponDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CouponDto>>> Handle(GetAllCouponsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all coupons");
        try
        {
            var coupons = await couponService.GetAllCouponsAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} coupons", coupons.Count);
            return ApiResponse<IReadOnlyList<CouponDto>>.Factory.Success(mapper.Map<IReadOnlyList<CouponDto>>(coupons));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all coupons");
            throw;
        }
    }
}
