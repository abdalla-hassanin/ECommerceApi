using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Queries;

public record GetActiveCouponsQuery : IRequest<ApiResponse<IReadOnlyList<CouponDto>>>;

public class GetActiveCouponsHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<GetActiveCouponsHandler> logger) : IRequestHandler<GetActiveCouponsQuery, ApiResponse<IReadOnlyList<CouponDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CouponDto>>> Handle(GetActiveCouponsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting active coupons");
        try
        {
            var coupons = await couponService.GetActiveCouponsAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} active coupons", coupons.Count);
            return ApiResponse<IReadOnlyList<CouponDto>>.Factory.Success(mapper.Map<IReadOnlyList<CouponDto>>(coupons));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting active coupons");
            throw;
        }
    }
}
