using ECommerceApi.Api.Base;
using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Wishlist;
using ECommerceApi.Core.MediatrHandlers.Wishlist.Commands;
using ECommerceApi.Core.MediatrHandlers.Wishlist.Queries;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WishlistController(IMediator mediator, ICustomerService customerService, ILogger<WishlistController> logger) : ControllerBase
{
    [HttpGet("customer/{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Wishlist for Customer",
        Description = "This endpoint allows Admins and Customers to retrieve a wishlist for a customer."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<WishlistDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<WishlistDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetWishlistForCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedWishlistResponseExample))]
    public async Task<IResult> GetWishlistForCustomer(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own Wishlists");
        }
        var query = new GetWishlistForCustomerQuery(customerId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("customer/{customerId}/product/{productId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Wishlist Item",
        Description = "This endpoint allows Admins and Customers to retrieve a wishlist item for a customer."
    )]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetWishlistItemResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundWishlistResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedWishlistResponseExample))]
    public async Task<IResult> GetWishlistItem(
        string customerId,
        string productId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own Wishlists");
        }
        var query = new GetWishlistItemQuery(customerId, productId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Add to Wishlist",
        Description = "This endpoint allows Admins and Customers to add a product to a wishlist."
    )]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<WishlistDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(AddToWishlistResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestWishlistResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedWishlistResponseExample))]
    public async Task<IResult> AddToWishlist(
        [FromBody] AddToWishlistCommand command,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, command.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can add to wishlist only for own account");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("customer/{customerId}/product/{productId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Remove from Wishlist",
        Description = "This endpoint allows Admins and Customers to remove a product from a wishlist."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(RemoveFromWishlistResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedWishlistResponseExample))]
    public async Task<IResult> RemoveFromWishlist(
        string customerId,
        string productId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can remove from wishlist only for own account");
        }
        var command = new RemoveFromWishlistCommand(customerId, productId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("customer/{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Clear Wishlist",
        Description = "This endpoint allows Admins and Customers to clear a wishlist."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ClearWishlistResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedWishlistResponseExample))]
    public async Task<IResult> ClearWishlist(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can clear wishlist only for own account");
        }
        var command = new ClearWishlistCommand(customerId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}