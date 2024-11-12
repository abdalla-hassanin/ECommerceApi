using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Order.Queries;

public record GetOrdersByCustomerIdQuery(string CustomerId) : IRequest<ApiResponse<IReadOnlyList<OrderDto>>>;

public class GetOrdersByCustomerIdHandler(
    IOrderService orderService,
    IMapper mapper,
    ILogger<GetOrdersByCustomerIdHandler> logger) : IRequestHandler<GetOrdersByCustomerIdQuery, ApiResponse<IReadOnlyList<OrderDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<OrderDto>>> Handle(GetOrdersByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting orders for customer with ID {CustomerId}", request.CustomerId);
        try
        {
            var orders = await orderService.GetOrdersByCustomerIdAsync(request.CustomerId, cancellationToken);
            logger.LogInformation("Retrieved {Count} orders for customer with ID {CustomerId}", orders.Count, request.CustomerId);
            return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(mapper.Map<IReadOnlyList<OrderDto>>(orders));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting orders for customer with ID {CustomerId}", request.CustomerId);
            throw;
        }
    }
}
