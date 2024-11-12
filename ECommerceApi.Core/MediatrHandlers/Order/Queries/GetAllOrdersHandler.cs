using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Queries;

public record GetAllOrdersQuery : IRequest<ApiResponse<IReadOnlyList<OrderDto>>>;

public class GetAllOrdersHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<GetAllOrdersHandler> logger) : IRequestHandler<GetAllOrdersQuery, ApiResponse<IReadOnlyList<OrderDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<OrderDto>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all orders");
        try
        {
            var orders = await orderService.GetAllOrdersAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} orders", orders.Count);
            return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all orders");
            throw;
        }
    }
}



