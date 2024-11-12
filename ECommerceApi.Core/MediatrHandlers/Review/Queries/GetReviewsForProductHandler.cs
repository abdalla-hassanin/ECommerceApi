using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Queries;

public record GetReviewsForProductQuery(string ProductId) : IRequest<ApiResponse<IReadOnlyList<ReviewDto>>>;

public class GetReviewsForProductHandler(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<GetReviewsForProductHandler> logger) 
    : IRequestHandler<GetReviewsForProductQuery, ApiResponse<IReadOnlyList<ReviewDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ReviewDto>>> Handle(GetReviewsForProductQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reviews for product {ProductId}", request.ProductId);

        try
        {
            var reviews = await reviewService.GetReviewsForProductAsync(request.ProductId, cancellationToken);
            logger.LogDebug("Retrieved {Count} reviews for product {ProductId}", reviews.Count, request.ProductId);

            var reviewDtos = mapper.Map<IReadOnlyList<ReviewDto>>(reviews);
            logger.LogDebug("Mapped Review entities to ReviewDto list");

            return ApiResponse<IReadOnlyList<ReviewDto>>.Factory.Success(reviewDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting reviews for product {ProductId}", request.ProductId);
            throw;
        }
    }
}