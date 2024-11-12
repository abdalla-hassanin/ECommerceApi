using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IReviewService
{
    Task<Review?> GetReviewByIdAsync(string reviewId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Review>> GetReviewsForProductAsync(string productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Review>> GetReviewsByCustomerAsync(string customerId, CancellationToken cancellationToken = default);
    Task<Review> CreateReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task<Review> UpdateReviewAsync(Review review, CancellationToken cancellationToken = default);
    Task DeleteReviewAsync(string reviewId, CancellationToken cancellationToken = default);
}