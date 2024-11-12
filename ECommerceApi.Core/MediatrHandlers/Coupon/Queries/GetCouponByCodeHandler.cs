using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Queries;

public record GetCouponByCodeQuery(string Code) : IRequest<ApiResponse<CouponDto>>;

public class GetCouponByCodeHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<GetCouponByCodeHandler> logger) : IRequestHandler<GetCouponByCodeQuery, ApiResponse<CouponDto>>
{
    public async Task<ApiResponse<CouponDto>> Handle(GetCouponByCodeQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting coupon by code {CouponCode}", request.Code);
        try
        {
            var coupon = await couponService.GetCouponByCodeAsync(request.Code, cancellationToken);
            if (coupon is null)
            {
                logger.LogWarning("Coupon with code {CouponCode} not found", request.Code);
                return ApiResponse<CouponDto>.Factory.NotFound("Coupon not found");
            }
            logger.LogInformation("Coupon with code {CouponCode} retrieved successfully", request.Code);
            return ApiResponse<CouponDto>.Factory.Success(mapper.Map<CouponDto>(coupon));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting coupon by code {CouponCode}", request.Code);
            throw;
        }
    }
}
