using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;

public record CreateOrderCommand(
    string CustomerId,
    string OrderNumber,
    string Status,
    decimal Subtotal,
    decimal Tax,
    decimal Shipping,
    decimal Total,
    string ShippingAddressId,
    string PaymentMethod,
    string ShippingMethod,
    string? CouponId,
    string? Notes,
    ICollection<CreateOrderItemCommand> OrderItems
) : IRequest<ApiResponse<OrderDto>>;

public record CreateOrderItemCommand(
    string ProductId,
    string? VariantId,
    int Quantity,
    decimal Price,
    decimal Subtotal,
    decimal Tax,
    decimal Total
);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Customer ID must be a valid ULID.");

        RuleFor(x => x.ShippingAddressId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Shipping address ID must be a valid ULID.");

        RuleFor(x => x.CouponId)
            .Length(26)
            .When(x => x != null)
            .WithMessage("Coupon ID must be a valid ULID.");

        RuleFor(x => x.OrderNumber)
            .NotEmpty()
            .WithMessage("Order number is required.")
            .MaximumLength(50)
            .WithMessage("Order number must not exceed 50 characters.");

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

        RuleFor(x => x.OrderItems)
            .NotEmpty()
            .WithMessage("At least one order item is required.");

        RuleForEach(x => x.OrderItems).SetValidator(new CreateOrderItemCommandValidator());
    }
}

public class CreateOrderItemCommandValidator : AbstractValidator<CreateOrderItemCommand>
{
    public CreateOrderItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("Product ID must be a valid ULID.");

        RuleFor(x => x.VariantId)
            .Length(26)
            .When(x => x != null)
            .WithMessage("Variant ID must be a valid ULID.");
        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than 0.");

        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Price must be greater than or equal to 0.");

        RuleFor(x => x.Subtotal)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Subtotal must be greater than or equal to 0.");

        RuleFor(x => x.Tax)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Tax must be greater than or equal to 0.");

        RuleFor(x => x.Total)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total must be greater than or equal to 0.");
    }
}

public class CreateOrderHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<CreateOrderHandler> logger) : IRequestHandler<CreateOrderCommand, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new order for customer {CustomerId}", request.CustomerId);
        try
        {
            var order = mapper.Map<Data.Entities.Order>(request);
            Data.Entities.Order createdOrder = await orderService.CreateOrderAsync(order, cancellationToken);
            logger.LogInformation("Order created successfully with ID {OrderId} for customer {CustomerId}",
                createdOrder.OrderId, request.CustomerId);
            return ApiResponse<OrderDto>.Factory.Created(mapper.Map<OrderDto>(createdOrder));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating order for customer {CustomerId}", request.CustomerId);
            throw;
        }
    }
}