using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<PaymentService> _logger;

    public PaymentService(IUnitOfWork unitOfWork, ILogger<PaymentService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Payment?> GetPaymentByIdAsync(string paymentId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting payment with ID: {PaymentId}", paymentId);
        return await _unitOfWork.Repository<Payment>().GetByIdAsync(paymentId.ToString(), cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetAllPaymentsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all payments");
        return await _unitOfWork.Repository<Payment>().ListAllAsync(cancellationToken);
    }

    public async Task<Payment> CreatePaymentAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new payment for order ID: {OrderId}", payment.OrderId);
        await _unitOfWork.Repository<Payment>().AddAsync(payment, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Payment created with ID: {PaymentId}", payment.PaymentId);
        return payment;
    }

    public async Task<Payment> UpdatePaymentAsync(Payment payment, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating payment with ID: {PaymentId}", payment.PaymentId);
        await _unitOfWork.Repository<Payment>().UpdateAsync(payment, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Payment updated successfully");
        return payment;
    }

    public async Task DeletePaymentAsync(string paymentId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting payment with ID: {PaymentId}", paymentId);
        var payment = await GetPaymentByIdAsync(paymentId, cancellationToken);
        if (payment is not null)
        {
            await _unitOfWork.Repository<Payment>().DeleteAsync(payment, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Payment deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent payment with ID: {PaymentId}", paymentId);
        }
    }

    public async Task<IReadOnlyList<Payment>> GetPaymentsByOrderIdAsync(string orderId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting payments for order ID: {OrderId}", orderId);
        var spec = new PaymentSpecifications.ByOrderId(orderId);
        return await _unitOfWork.Repository<Payment>().ListAsync(spec, cancellationToken);
    }

    public async Task<Payment> UpdatePaymentStatusAsync(string paymentId, string newStatus, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating status of payment with ID: {PaymentId} to {NewStatus}", paymentId, newStatus);
        var payment = await GetPaymentByIdAsync(paymentId, cancellationToken);
        if (payment is not null)
        {
            payment.Status = newStatus;
            payment.UpdatedAt = DateTime.UtcNow;
            await UpdatePaymentAsync(payment, cancellationToken);
            _logger.LogInformation("Payment status updated successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to update status of non-existent payment with ID: {PaymentId}", paymentId);
        }
        return payment!;
    }

    public async Task<IReadOnlyList<Payment>> GetPaymentsByStatusAsync(string status, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting payments with status: {Status}", status);
        var spec = new PaymentSpecifications.ByStatus(status);
        return await _unitOfWork.Repository<Payment>().ListAsync(spec, cancellationToken);
    }

    public async Task<Payment> ProcessStripePaymentAsync(string orderId, string paymentIntentId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Processing Stripe payment for order ID: {OrderId}", orderId);
        await _unitOfWork.BeginTransactionAsync(cancellationToken);

        try
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(orderId, cancellationToken);
            if (order is null)
            {
                _logger.LogWarning("Order with ID {OrderId} not found", orderId);
                throw new KeyNotFoundException($"Order with ID {orderId} not found");
            }

            var payment = new Payment
            {
                OrderId = orderId,
                PaymentMethod = "Stripe",
                Amount = order.Total,
                Status = "Processing",
                StripePaymentIntentId = paymentIntentId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Payment>().AddAsync(payment, cancellationToken);

            order.Status = "PaymentProcessing";
            await _unitOfWork.Repository<Order>().UpdateAsync(order, cancellationToken);

            await _unitOfWork.CompleteAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);

            _logger.LogInformation("Stripe payment processed successfully for order ID: {OrderId}", orderId);
            return payment;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error processing Stripe payment for order ID: {OrderId}", orderId);
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }
}