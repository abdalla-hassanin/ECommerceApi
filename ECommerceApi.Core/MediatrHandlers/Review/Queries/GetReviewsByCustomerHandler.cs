using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Queries;

public record GetReviewsByCustomerQuery(string CustomerId) : IRequest<ApiResponse<IReadOnlyList<ReviewDto>>>;

public class GetReviewsByCustomerHandler(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<GetReviewsByCustomerHandler> logger) 
    : IRequestHandler<GetReviewsByCustomerQuery, ApiResponse<IReadOnlyList<ReviewDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<ReviewDto>>> Handle(GetReviewsByCustomerQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting reviews for customer {CustomerId}", request.CustomerId);

        try
        {
            var reviews = await reviewService.GetReviewsByCustomerAsync(request.CustomerId, cancellationToken);
            logger.LogDebug("Retrieved {Count} reviews for customer {CustomerId}", reviews.Count, request.CustomerId);

            var reviewDtos = mapper.Map<IReadOnlyList<ReviewDto>>(reviews);
            logger.LogDebug("Mapped Review entities to ReviewDto list");

            return ApiResponse<IReadOnlyList<ReviewDto>>.Factory.Success(reviewDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting reviews for customer {CustomerId}", request.CustomerId);
            throw;
        }
    }
}