using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Queries;


public record GetCouponByIdQuery(string CouponId) : IRequest<ApiResponse<CouponDto>>;

public class GetCouponByIdHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<GetCouponByIdHandler> logger) : IRequestHandler<GetCouponByIdQuery, ApiResponse<CouponDto>>
{
    public async Task<ApiResponse<CouponDto>> Handle(GetCouponByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting coupon by ID {CouponId}", request.CouponId);
        try
        {
            var coupon = await couponService.GetCouponByIdAsync(request.CouponId, cancellationToken);
            if (coupon is null)
            {
                logger.LogWarning("Coupon with ID {CouponId} not found", request.CouponId);
                return ApiResponse<CouponDto>.Factory.NotFound("Coupon not found");
            }
            logger.LogInformation("Coupon with ID {CouponId} retrieved successfully", request.CouponId);
            return ApiResponse<CouponDto>.Factory.Success(mapper.Map<CouponDto>(coupon));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting coupon by ID {CouponId}", request.CouponId);
            throw;
        }
    }
}
