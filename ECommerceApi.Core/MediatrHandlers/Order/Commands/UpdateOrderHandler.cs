using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;

public record UpdateOrderCommand(
    string OrderId,
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal Shipping,
    decimal Total,
    string ShippingAddressId,
    string PaymentMethod,
    string ShippingMethod,
    string? CouponId,
    string? Notes
) : IRequest<ApiResponse<OrderDto>>;

public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderCommandValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Order ID must be a valid ULID.");

        RuleFor(x => x.ShippingAddressId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Shipping address ID must be a valid ULID.");

        RuleFor(x => x.CouponId)
            .Length(26)
            .When(x => x != null)
            .WithMessage("Coupon ID must be a valid ULID.");
        RuleFor(x => x.Status)
            .NotEmpty()
            .WithMessage("Order status is required.")
            .MaximumLength(20)
            .WithMessage("Order status must not exceed 20 characters.");

        RuleFor(x => x.Subtotal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Subtotal must be greater than or equal to 0.");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax must be greater than or equal to 0.");

        RuleFor(x => x.Shipping)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Shipping must be greater than or equal to 0.");

        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total must be greater than or equal to 0.");
        
        RuleFor(x => x.PaymentMethod)
            .NotEmpty()
            .WithMessage("Payment method is required.")
            .MaximumLength(50)
            .WithMessage("Payment method must not exceed 50 characters.");

        RuleFor(x => x.ShippingMethod)
            .NotEmpty()
            .WithMessage("Shipping method is required.")
            .MaximumLength(50)
            .WithMessage("Shipping method must not exceed 50 characters.");
    }
}

public class UpdateOrderHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<UpdateOrderHandler> logger) : IRequestHandler<UpdateOrderCommand, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating order with ID {OrderId}", request.OrderId);
        try
        {
            var existingOrder = await orderService.GetOrderByIdAsync(request.OrderId, cancellationToken);
            if (existingOrder is null)
            {
                logger.LogWarning("Order with ID {OrderId} not found", request.OrderId);
                return ApiResponse<OrderDto>.Factory.NotFound("Order not found");
            }

            mapper.Map(request, existingOrder);
            var updatedOrder = await orderService.UpdateOrderAsync(existingOrder, cancellationToken);
            logger.LogInformation("Order with ID {OrderId} updated successfully", request.OrderId);
            return ApiResponse<OrderDto>.Factory.Success(mapper.Map<OrderDto>(updatedOrder));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating order with ID {OrderId}", request.OrderId);
            throw;
        }
    }
}
