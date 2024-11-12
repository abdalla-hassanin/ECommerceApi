using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Queries;

public record GetOrderByIdQuery(string OrderId) : IRequest<ApiResponse<OrderDto>>;

public class GetOrderByIdHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<GetOrderByIdHandler> logger) : IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderDto>>
{
    public async Task<ApiResponse<OrderDto>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting order with ID {OrderId}", request.OrderId);
        try
        {
            var order = await orderService.GetOrderByIdAsync(request.OrderId, cancellationToken);
            if (order is null)
            {
                logger.LogWarning("Order with ID {OrderId} not found", request.OrderId);
                return ApiResponse<OrderDto>.Factory.NotFound("Order not found");
            }
            logger.LogInformation("Order with ID {OrderId} retrieved successfully", request.OrderId);
            return ApiResponse<OrderDto>.Factory.Success(mapper.Map<OrderDto>(order));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting order with ID {OrderId}", request.OrderId);
            throw;
        }
    }
}
