using ECommerceApi.Api.Base;
using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Review;
using ECommerceApi.Core.MediatrHandlers.Review.Commands;
using ECommerceApi.Core.MediatrHandlers.Review.Queries;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController(IMediator mediator, ICustomerService customerService, ILogger<ReviewController> logger) : ControllerBase
{
    [HttpGet("{reviewId}")]
    [SwaggerOperation(
        Summary = "Get Review by ID",
        Description = "This endpoint allows Admins, Customers and public users to retrieve a review by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetReviewByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> GetReviewById(
        string reviewId,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewByIdQuery(reviewId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("product/{productId}")]
    [SwaggerOperation(
        Summary = "Get Reviews for Product",
        Description = "This endpoint allows Admins, Customers and public users to retrieve reviews for a product."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReviewDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReviewDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetReviewsForProductResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> GetReviewsForProduct(
        string productId,
        CancellationToken cancellationToken)
    {
        var query = new GetReviewsForProductQuery(productId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("customer/{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Reviews by Customer",
        Description = "This endpoint allows Admins and Customers to retrieve reviews by a customer."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReviewDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ReviewDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetReviewsByCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> GetReviewsByCustomer(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own reviews");
        }

        var query = new GetReviewsByCustomerQuery(customerId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Create Review",
        Description = "This endpoint allows Admins and Customers to create a review."
    )]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> CreateReview(
        [FromBody] CreateReviewCommand command,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, command.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can create reviews only for own account");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{reviewId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Update Review",
        Description = "This endpoint allows Admins and Customers to update a review."
    )]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<ReviewDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> UpdateReview(
        string reviewId,
        [FromBody] UpdateReviewCommand command,
        CancellationToken cancellationToken)
    {
        if (reviewId != command.ReviewId)
        {
            return ApiResponseResults.BadRequest("Review ID mismatch");
        }
        // First get the review to check ownership
        var query = new GetReviewByIdQuery(reviewId);
        var reviewResult = await mediator.Send(query, cancellationToken);
        
        if (!reviewResult.IsSuccess || reviewResult.Data == null)
        {
            return reviewResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, reviewResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can update only own reviews");
        }


        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{reviewId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Delete Review",
        Description = "This endpoint allows Admins and Customers to delete a review."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteReviewResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedReviewResponseExample))]
    public async Task<IResult> DeleteReview(
        string reviewId,
        CancellationToken cancellationToken)
    {
        // First get the review to check ownership
        var query = new GetReviewByIdQuery(reviewId);
        var reviewResult = await mediator.Send(query, cancellationToken);
        
        if (!reviewResult.IsSuccess || reviewResult.Data == null)
        {
            return reviewResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, reviewResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can delete only own reviews");
        }


        var command = new DeleteReviewCommand(reviewId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}