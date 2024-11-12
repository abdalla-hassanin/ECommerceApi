using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;
public record DeleteOrderItemCommand(string OrderId, string OrderItemId) : IRequest<ApiResponse<OrderDto>>;

public class DeleteOrderItemCommandValidator : AbstractValidator<DeleteOrderItemCommand>
{
    public DeleteOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().Length(26).WithMessage("Order ID must be a valid ULID.");
        RuleFor(x => x.OrderItemId).NotEmpty().Length(26).WithMessage("Order Item ID must be a valid ULID.");
    }
}

public class DeleteOrderItemHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<DeleteOrderItemHandler> logger) : IRequestHandler<DeleteOrderItemCommand, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting order item {OrderItemId} from order {OrderId}", request.OrderItemId, request.OrderId);
        try
        {
            var updatedOrder = await orderService.DeleteOrderItemAsync(request.OrderId, request.OrderItemId, cancellationToken);
            logger.LogInformation("Order item {OrderItemId} deleted successfully from order {OrderId}", request.OrderItemId, request.OrderId);
            return ApiResponse<OrderDto>.Factory.Success(mapper.Map<OrderDto>(updatedOrder));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting order item {OrderItemId} from order {OrderId}", request.OrderItemId, request.OrderId);
            throw;
        }
    }
}
