using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Commands;

public record DeleteReviewCommand(string ReviewId) : IRequest<ApiResponse<bool>>;

public class DeleteReviewHandler(
    IReviewService reviewService,
    ILogger<DeleteReviewHandler> logger) 
    : IRequestHandler<DeleteReviewCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting review {ReviewId}", request.ReviewId);

        try
        {
            await reviewService.DeleteReviewAsync(request.ReviewId, cancellationToken);
            logger.LogInformation("Review {ReviewId} deleted successfully", request.ReviewId);
            return ApiResponse<bool>.Factory.Success(true, "Review deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting review {ReviewId}", request.ReviewId);
            throw;
        }
    }
}