using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Queries;

public record GetOrdersByStatusQuery(string Status) : IRequest<ApiResponse<IReadOnlyList<OrderDto>>>;

public class GetOrdersByStatusHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<GetOrdersByStatusHandler> logger) : IRequestHandler<GetOrdersByStatusQuery, ApiResponse<IReadOnlyList<OrderDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<OrderDto>>> Handle(GetOrdersByStatusQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting orders with status {Status}", request.Status);
        try
        {
            var orders = await orderService.GetOrdersByStatusAsync(request.Status, cancellationToken);
            logger.LogInformation("Retrieved {Count} orders with status {Status}", orders.Count, request.Status);
            return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting orders with status {Status}", request.Status);
            throw;
        }
    }
}
