using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Coupon.Commands;

public record CreateCouponCommand(
    string Code,
    string Description,
    string DiscountType,
    decimal DiscountValue,
    decimal? MinimumPurchase,
    int? UsageLimit,
    DateTime? StartDate,
    DateTime? EndDate,
    bool IsActive
) : IRequest<ApiResponse<CouponDto>>;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
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

public class CreateCouponHandler(
    ICouponService couponService,
    IMapper mapper,
    ILogger<CreateCouponHandler> logger) : IRequestHandler<CreateCouponCommand, ApiResponse<CouponDto>>
{
    public async Task<ApiResponse<CouponDto>> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new coupon with code {CouponCode}", request.Code);
        try
        {
            var coupon = mapper.Map<Data.Entities.Coupon>(request);
            Data.Entities.Coupon createdCoupon = await couponService.CreateCouponAsync(coupon, cancellationToken);
            logger.LogInformation("Coupon created successfully with ID {CouponId}", createdCoupon.CouponId);
            return ApiResponse<CouponDto>.Factory.Created(mapper.Map<CouponDto>(createdCoupon));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating coupon with code {CouponCode}", request.Code);
            throw;
        }
    }
}