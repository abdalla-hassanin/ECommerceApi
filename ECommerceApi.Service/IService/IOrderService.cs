using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IOrderService
{
    Task<Order?> GetOrderByIdAsync(string orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Order>> GetOrdersByStatusAsync(string status, CancellationToken cancellationToken = default);
    
    
    
    Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken = default);
    Task DeleteOrderAsync(string orderId, CancellationToken cancellationToken = default);
    Task<Order> AddOrderItemAsync(string orderId, OrderItem item, CancellationToken cancellationToken = default);
    Task<Order> UpdateOrderItemAsync(string orderId, OrderItem item, CancellationToken cancellationToken = default);
    Task<Order> DeleteOrderItemAsync(string orderId, string itemId, CancellationToken cancellationToken = default);
}