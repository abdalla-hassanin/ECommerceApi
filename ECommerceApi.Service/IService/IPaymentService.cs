using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IPaymentService
{
    Task<Payment?> GetPaymentByIdAsync(string paymentId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetPaymentsByOrderIdAsync(string orderId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Payment>> GetPaymentsByStatusAsync(string status, CancellationToken cancellationToken = default);
    Task<Payment> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken = default);
    Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken = default);
    Task DeletePaymentAsync(string paymentId, CancellationToken cancellationToken = default);
    Task<Payment> UpdatePaymentStatusAsync(string paymentId, string newStatus, CancellationToken cancellationToken = default);
    Task<Payment> ProcessStripePaymentAsync(string orderId, string paymentIntentId, CancellationToken cancellationToken = default);
}