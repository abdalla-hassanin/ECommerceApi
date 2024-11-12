using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Commands;

public record AddOrderItemCommand(
    string OrderId,
    string ProductId,
    string? VariantId,
    int Quantity,
    decimal Price,
    decimal Subtotal,
    decimal Tax,
    decimal Total
) : IRequest<ApiResponse<OrderDto>>;

public class AddOrderItemCommandValidator : AbstractValidator<AddOrderItemCommand>
{
    public AddOrderItemCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty().Length(26).WithMessage("Order ID must be a valid ULID.");
        RuleFor(x => x.ProductId).NotEmpty().Length(26).WithMessage("Product ID must be a valid ULID.");
        RuleFor(x => x.VariantId).Length(26).When(x => x != null).WithMessage("Variant ID must be a valid ULID.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");
        RuleFor(x => x.Subtotal).GreaterThanOrEqualTo(0).WithMessage("Subtotal must be greater than or equal to 0.");
        RuleFor(x => x.Tax).GreaterThanOrEqualTo(0).WithMessage("Tax must be greater than or equal to 0.");
        RuleFor(x => x.Total).GreaterThanOrEqualTo(0).WithMessage("Total must be greater than or equal to 0.");
    }
}

public class AddOrderItemHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<AddOrderItemHandler> logger) : IRequestHandler<AddOrderItemCommand, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(AddOrderItemCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding order item to order with ID {OrderId}", request.OrderId);
        try
        {
            var orderItem = mapper.Map<Data.Entities.OrderItem>(request);
            var updatedOrder = await orderService.AddOrderItemAsync(request.OrderId, orderItem, cancellationToken);
            logger.LogInformation("Order item added successfully to order with ID {OrderId}", request.OrderId);
            return ApiResponse<OrderDto>.Factory.Success(mapper.Map<OrderDto>(updatedOrder));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while adding order item to order with ID {OrderId}", request.OrderId);
            throw;
        }
    }
}
