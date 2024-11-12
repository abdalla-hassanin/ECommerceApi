using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;

public record UpdateOrderItemCommand(
    string OrderId,
    string OrderItemId,
    int Quantity,
    decimal Price,
    decimal Subtotal,
    decimal Tax,
    decimal Total
) : IRequest<ApiResponse<OrderDto>>;

public class UpdateOrderItemCommandValidator : AbstractValidator<UpdateOrderItemCommand>
{
    public UpdateOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().Length(26).WithMessage("Order ID must be a valid ULID.");
        RuleFor(x => x.OrderItemId).NotEmpty().Length(26).WithMessage("Order Item ID must be a valid ULID.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
        RuleFor(x => x.Subtotal).GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to 0.");
        RuleFor(x => x.Tax).GreaterThanOrEqualTo(0).WithMessage("Tax must be greater than or equal to 0.");
        RuleFor(x => x.Total).GreaterThanOrEqualTo(0).WithMessage("Total must be greater than or equal to 0.");
    }
}

public class UpdateOrderItemHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<UpdateOrderItemHandler> logger) : IRequestHandler<UpdateOrderItemCommand, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating order item {OrderItemId} in order {OrderId}", request.OrderItemId, request.OrderId);
        try
        {
            var orderItem = mapper.Map<Data.Entities.OrderItem>(request);
            var updatedOrder = await orderService.UpdateOrderItemAsync(request.OrderId, orderItem, cancellationToken);
            logger.LogInformation("Order item {OrderItemId} updated successfully in order {OrderId}", request.OrderItemId, request.OrderId);
            return ApiResponse<OrderDto>.Factory.Success(mapper.Map<OrderDto>(updatedOrder));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating order item {OrderItemId} in order {OrderId}", request.OrderItemId, request.OrderId);
            throw;
        }
    }
}
