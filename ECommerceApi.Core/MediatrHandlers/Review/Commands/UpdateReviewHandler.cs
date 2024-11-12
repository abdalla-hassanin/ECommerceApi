using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Commands;

public record UpdateReviewCommand(
    string ReviewId,
    int Rating,
    string Title,
    string Content,
    string Status
) : IRequest<ApiResponse<ReviewDto>>;

public class UpdateReviewCommandValidator : AbstractValidator<UpdateReviewCommand>
{
    public UpdateReviewCommandValidator()
    {
        RuleFor(x => x.ReviewId)
            .NotEmpty().WithMessage("ReviewId is required.")
            .Length(26).WithMessage("ReviewId must be a valid Ulid (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("ReviewId must be a valid Ulid format.");
            
        RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100).WithMessage("Title is required and must not exceed 100 characters.");
        RuleFor(x => x.Content).NotEmpty().MaximumLength(1000).WithMessage("Content is required and must not exceed 1000 characters.");
        RuleFor(x => x.Status).NotEmpty().MaximumLength(20).WithMessage("Status is required and must not exceed 20 characters.");
    }
}

public class UpdateReviewHandler(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<UpdateReviewHandler> logger) 
    : IRequestHandler<UpdateReviewCommand, ApiResponse<ReviewDto>>
{
    public async Task<ApiResponse<ReviewDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating review {ReviewId}", request.ReviewId);

        try
        {
            var existingReview = await reviewService.GetReviewByIdAsync(request.ReviewId, cancellationToken);
            if (existingReview is null)
            {
                logger.LogWarning("Review {ReviewId} not found", request.ReviewId);
                return ApiResponse<ReviewDto>.Factory.NotFound("Review not found");
            }

            mapper.Map(request, existingReview);
            logger.LogDebug("Mapped UpdateReviewCommand to existing Review entity");

            var updatedReview = await reviewService.UpdateReviewAsync(existingReview, cancellationToken);
            logger.LogInformation("Review {ReviewId} updated successfully", request.ReviewId);

            var reviewDto = mapper.Map<ReviewDto>(updatedReview);
            logger.LogDebug("Mapped updated Review entity to ReviewDto");

            return ApiResponse<ReviewDto>.Factory.Success(reviewDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating review {ReviewId}", request.ReviewId);
            throw;
        }
    }
}