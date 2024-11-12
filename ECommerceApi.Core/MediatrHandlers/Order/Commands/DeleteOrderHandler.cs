using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;

public record DeleteOrderCommand(string OrderId) : IRequest<ApiResponse<bool>>;

public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().WithMessage("Order ID is required.");
    }
}

public class DeleteOrderHandler(
    IOrderService orderService,
    ILogger<DeleteOrderHandler> logger) : IRequestHandler<DeleteOrderCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting order with ID {OrderId}", request.OrderId);
        try
        {
            await orderService.DeleteOrderAsync(request.OrderId, cancellationToken);
            logger.LogInformation("Order with ID {OrderId} deleted successfully", request.OrderId);
            return ApiResponse<bool>.Factory.Success(true, "Order deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting order with ID {OrderId}", request.OrderId);
            throw;
        }
    }
}
