using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Coupon;
using ECommerceApi.Core.MediatrHandlers.Coupon.Commands;
using ECommerceApi.Core.MediatrHandlers.Coupon.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CouponController : ControllerBase
{
    private readonly IMediator _mediator;

    public CouponController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{couponId}")]
    [Authorize(Roles="Admin")]
    [SwaggerOperation(
        Summary = "Get Coupon by ID",
        Description = "This endpoint allows Admins to retrieve a coupon by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCouponByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> GetCouponById(
        string couponId,
        CancellationToken cancellationToken)
    {
        var query = new GetCouponByIdQuery(couponId);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet]
    [Authorize(Roles="Admin")]
    [SwaggerOperation(
        Summary = "Get All Coupons",
        Description = "This endpoint allows Admins to retrieve all coupons."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CouponDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CouponDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllCouponsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> GetAllCoupons(CancellationToken cancellationToken)
    {
        var query = new GetAllCouponsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("active")]
    [Authorize(Roles="Admin")]
    [SwaggerOperation(
        Summary = "Get Active Coupons",
        Description = "This endpoint allows Admins to retrieve all active coupons."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CouponDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CouponDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetActiveCouponsResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> GetActiveCoupons(CancellationToken cancellationToken)
    {
        var query = new GetActiveCouponsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("code/{code}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Coupon by Code",
        Description = "This endpoint allows Admins and Customers to retrieve a coupon by its code."
    )]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCouponByCodeResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> GetCouponByCode(
        string code,
        CancellationToken cancellationToken)
    {
        var query = new GetCouponByCodeQuery(code);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("validate")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Validate Coupon",
        Description = "This endpoint allows Admins and Customers to validate a coupon."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(ValidCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(InvalidCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> ValidateCoupon(
        [FromQuery] string code,
        [FromQuery] decimal purchaseAmount,
        CancellationToken cancellationToken)
    {
        var query = new ValidateCouponQuery(code, purchaseAmount);
        var result = await _mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Create New Coupon",
        Description = "This endpoint allows Admins to create a new coupon."
    )]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> CreateCoupon(
        [FromBody] CreateCouponCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{couponId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Update Coupon",
        Description = "This endpoint allows Admins to update a coupon."
    )]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CouponDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> UpdateCoupon(
        string couponId,
        [FromBody] UpdateCouponCommand command,
        CancellationToken cancellationToken)
    {
        if (couponId != command.CouponId)
        {
            return ApiResponseResults.BadRequest("Coupon ID mismatch");
        }

        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{couponId}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Delete Coupon",
        Description = "This endpoint allows Admins to delete a coupon."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteCouponResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCouponResponseExample))]
    public async Task<IResult> DeleteCoupon(
        string couponId,
        CancellationToken cancellationToken)
    {
        var command = new DeleteCouponCommand(couponId);
        var result = await _mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}

