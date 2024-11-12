using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Review.Commands;

public record CreateReviewCommand(
    string ProductId,
    string CustomerId,
    string OrderId,
    int Rating,
    string Title,
    string Content,
    string Status
) : IRequest<ApiResponse<ReviewDto>>;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .Length(26)
            .WithMessage("ProductId must be a valid ULID.");
            
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .Length(26)
            .WithMessage("CustomerId must be a valid ULID.");
            
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .Length(26)
            .WithMessage("OrderId must be a valid ULID.");
        RuleFor(x => x.Rating).InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100).WithMessage("Title is required and must not exceed 100 characters.");
        RuleFor(x => x.Content).NotEmpty().MaximumLength(1000).WithMessage("Content is required and must not exceed 1000 characters.");
        RuleFor(x => x.Status).NotEmpty().MaximumLength(20).WithMessage("Status is required and must not exceed 20 characters.");
    }
}

public class CreateReviewHandler(
    IReviewService reviewService,
    IMapper mapper,
    ILogger<CreateReviewHandler> logger) 
    : IRequestHandler<CreateReviewCommand, ApiResponse<ReviewDto>>
{
    public async Task<ApiResponse<ReviewDto>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new review for product {ProductId} by customer {CustomerId}", request.ProductId, request.CustomerId);

        try
        {
            var review = mapper.Map<Data.Entities.Review>(request);
            logger.LogDebug("Mapped CreateReviewCommand to Review entity");

            var createdReview = await reviewService.CreateReviewAsync(review, cancellationToken);
            logger.LogInformation("Review created successfully for product {ProductId}", request.ProductId);

            var reviewDto = mapper.Map<ReviewDto>(createdReview);
            logger.LogDebug("Mapped created Review entity to ReviewDto");

            return ApiResponse<ReviewDto>.Factory.Created(reviewDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating review for product {ProductId}", request.ProductId);
            throw;
        }
    }
}