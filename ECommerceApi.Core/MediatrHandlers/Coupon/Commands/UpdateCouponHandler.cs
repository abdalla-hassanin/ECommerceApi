using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Commands;

public record UpdateCouponCommand(
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
    bool IsActive
) : IRequest<ApiResponse<CouponDto>>;

public class UpdateCouponCommandValidator : AbstractValidator<UpdateCouponCommand>
{
    public UpdateCouponCommandValidator()
    {
        RuleFor(x=>x.CouponId).NotEmpty().WithMessage("CouponId is required.")
            .Length(26).WithMessage("CouponId must be a valid Ulid (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("CouponId must be a valid Ulid format.");
        
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

        RuleFor(x => x.DiscountType)
            .NotEmpty().WithMessage("Discount type is required.")
            .Must(x => x == "Percentage" || x == "FixedAmount")
            .WithMessage("Discount type must be either 'Percentage' or 'FixedAmount'.");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0.");

        RuleFor(x => x.MinimumPurchase)
            .GreaterThanOrEqualTo(0).When(x => x.MinimumPurchase.HasValue)
            .WithMessage("Minimum purchase must be greater than or equal to 0.");

        RuleFor(x => x.UsageLimit)
            .GreaterThan(0).When(x => x.UsageLimit.HasValue).WithMessage("Usage limit must be greater than 0.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).When(x => x.StartDate.HasValue && x.EndDate.HasValue)
            .WithMessage("Start date must be before end date.");
    }
}

public class UpdateCouponHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<UpdateCouponHandler> logger) : IRequestHandler<UpdateCouponCommand, ApiResponse<CouponDto>>
{
    public async Task<ApiResponse<CouponDto>> Handle(UpdateCouponCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating coupon with ID {CouponId}", request.CouponId);
        try
        {
            var existingCoupon = await couponService.GetCouponByIdAsync(request.CouponId, cancellationToken);
            if (existingCoupon is null)
            {
                logger.LogWarning("Coupon with ID {CouponId} not found", request.CouponId);
                return ApiResponse<CouponDto>.Factory.NotFound("Coupon not found");
            }

            mapper.Map(request, existingCoupon);
            var updatedCoupon = await couponService.UpdateCouponAsync(existingCoupon, cancellationToken);
            logger.LogInformation("Coupon with ID {CouponId} updated successfully", request.CouponId);
            return ApiResponse<CouponDto>.Factory.Success(mapper.Map<CouponDto>(updatedCoupon));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating coupon with ID {CouponId}", request.CouponId);
            throw;
        }
    }
}
