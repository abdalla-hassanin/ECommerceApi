
using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Queries;

public record GetReviewByIdQuery(string ReviewId) : IRequest<ApiResponse<ReviewDto>>;

public class GetReviewByIdHandler(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<GetReviewByIdHandler> logger) 
    : IRequestHandler<GetReviewByIdQuery, ApiResponse<ReviewDto>>
{
    public async Task<ApiResponse<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting review {ReviewId}", request.ReviewId);

        try
        {
            var review = await reviewService.GetReviewByIdAsync(request.ReviewId, cancellationToken);
            if (review is null)
            {
                logger.LogWarning("Review {ReviewId} not found", request.ReviewId);
                return ApiResponse<ReviewDto>.Factory.NotFound("Review not found");
            }

            var reviewDto = mapper.Map<ReviewDto>(review);
            logger.LogDebug("Mapped Review entity to ReviewDto");

            return ApiResponse<ReviewDto>.Factory.Success(reviewDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting review {ReviewId}", request.ReviewId);
            throw;
        }
    }
}