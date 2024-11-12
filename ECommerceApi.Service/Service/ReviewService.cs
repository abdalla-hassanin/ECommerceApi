using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ReviewService> _logger;

    public ReviewService(IUnitOfWork unitOfWork, ILogger<ReviewService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Review?> GetReviewByIdAsync(string reviewId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting review with ID: {ReviewId}", reviewId);
        var spec = new ReviewSpecifications.ByReviewId(reviewId);
        var reviews = await _unitOfWork.Repository<Review>().ListAsync(spec, cancellationToken);
        return reviews.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Review>> GetReviewsForProductAsync(string productId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting reviews for product ID: {ProductId}", productId);
        var spec = new ReviewSpecifications.ByProductId(productId);
        return await _unitOfWork.Repository<Review>().ListAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<Review>> GetReviewsByCustomerAsync(string customerId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting reviews by customer ID: {CustomerId}", customerId);
        var spec = new ReviewSpecifications.ByCustomerId(customerId);
        return await _unitOfWork.Repository<Review>().ListAsync(spec, cancellationToken);
    }

    public async Task<Review> CreateReviewAsync(Review review, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new review for product ID: {ProductId} by customer ID: {CustomerId}", review.ProductId, review.CustomerId);
        await _unitOfWork.Repository<Review>().AddAsync(review, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Review created with ID: {ReviewId}", review.ReviewId);
        return review;
    }

    public async Task<Review> UpdateReviewAsync(Review review, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating review with ID: {ReviewId}", review.ReviewId);
        await _unitOfWork.Repository<Review>().UpdateAsync(review, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Review updated successfully");
        return review;
    }

    public async Task DeleteReviewAsync(string reviewId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting review with ID: {ReviewId}", reviewId);
        var review = await GetReviewByIdAsync(reviewId, cancellationToken);
        if (review is not null)
        {
            await _unitOfWork.Repository<Review>().DeleteAsync(review, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Review deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent review with ID: {ReviewId}", reviewId);
        }
    }
}