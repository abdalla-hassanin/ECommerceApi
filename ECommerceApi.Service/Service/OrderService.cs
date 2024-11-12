using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class OrderService : IOrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<OrderService> _logger;

    public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Order?> GetOrderByIdAsync(string orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting order with ID: {OrderId}", orderId);
        var spec = new OrderSpecifications.GetOrderById(orderId);
        return await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetAllOrdersAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all orders");
        var spec = new OrderSpecifications.GetOrderWithDetails();
        return await _unitOfWork.Repository<Order>().ListAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByCustomerIdAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting orders for customer ID: {CustomerId}", customerId);
        var spec = new OrderSpecifications.GetOrdersByCustomerId(customerId);
        return await _unitOfWork.Repository<Order>().ListAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<Order>> GetOrdersByStatusAsync(string status, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting orders with status: {Status}", status);
        var spec = new OrderSpecifications.GetOrdersByStatus(status);
        return await _unitOfWork.Repository<Order>().ListAsync(spec, cancellationToken);
    }

    public async Task<Order> CreateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new order for customer ID: {CustomerId}", order.CustomerId);
        await _unitOfWork.Repository<Order>().AddAsync(order, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Order created with ID: {OrderId}", order.OrderId);
        return order;
    }

    public async Task<Order> UpdateOrderAsync(Order order, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating order with ID: {OrderId}", order.OrderId);
        await _unitOfWork.Repository<Order>().UpdateAsync(order, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Order updated successfully");
        return order;
    }

    public async Task DeleteOrderAsync(string orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting order with ID: {OrderId}", orderId);
        var order = await GetOrderByIdAsync(orderId, cancellationToken);
        if (order is not null)
        {
            await _unitOfWork.Repository<Order>().DeleteAsync(order, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Order deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent order with ID: {OrderId}", orderId);
        }
    }

    public async Task<Order> AddOrderItemAsync(string orderId, OrderItem item, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Adding item to order with ID: {OrderId}", orderId);
        var order = await GetOrderByIdAsync(orderId, cancellationToken);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", orderId);
            throw new ArgumentException($"Order with id {orderId} not found");
        }
        order.OrderItems.Add(item);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Item added to order successfully");
        return order;
    }

    public async Task<Order> UpdateOrderItemAsync(string orderId, OrderItem item, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating item in order with ID: {OrderId}", orderId);
        var order = await GetOrderByIdAsync(orderId, cancellationToken);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", orderId);
            throw new ArgumentException($"Order with id {orderId} not found");
        }
        var existingItem = order.OrderItems.FirstOrDefault(i => i.OrderItemId == item.OrderItemId);
        if (existingItem == null)
        {
            _logger.LogWarning("Order item with ID {OrderItemId} not found in order {OrderId}", item.OrderItemId, orderId);
            throw new ArgumentException($"Order item with id {item.OrderItemId} not found in order {orderId}");
        }
        // Update the existing item properties
        existingItem.Quantity = item.Quantity;
        existingItem.Price = item.Price;
        existingItem.Subtotal = item.Subtotal;
        existingItem.Tax = item.Tax;
        existingItem.Total = item.Total;
        existingItem.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Order item updated successfully");
        return order;
    }

    public async Task<Order> DeleteOrderItemAsync(string orderId, string itemId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting item from order with ID: {OrderId}", orderId);
        var order = await GetOrderByIdAsync(orderId, cancellationToken);
        if (order == null)
        {
            _logger.LogWarning("Order with ID {OrderId} not found", orderId);
            throw new ArgumentException($"Order with id {orderId} not found");
        }
        var item = order.OrderItems.FirstOrDefault(i => i.OrderItemId == itemId);
        if (item == null)
        {
            _logger.LogWarning("Order item with ID {OrderItemId} not found in order {OrderId}", itemId, orderId);
            throw new ArgumentException($"Order item with id {itemId} not found in order {orderId}");
        }
        order.OrderItems.Remove(item);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Order item deleted successfully");
        return order;
    }
}