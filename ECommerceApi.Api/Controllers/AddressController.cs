using ECommerceApi.Api.Base;
using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Address;
using ECommerceApi.Core.MediatrHandlers.Address.Commands;
using ECommerceApi.Core.MediatrHandlers.Address.Queries;
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
public class AddressController(IMediator mediator, ICustomerService customerService, ILogger<AddressController> logger)
    : ControllerBase
{
    [HttpGet("{addressId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Address by ID",
        Description = "This endpoint allows Admins and Customers to retrieve an address by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAddressByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAddressResponseExample))]
    public async Task<IResult> GetAddressById(
        string addressId,
        CancellationToken cancellationToken)
    {
        var query = new GetAddressByIdQuery(addressId);
        ApiResponse<AddressDto> result = await mediator.Send(query, cancellationToken);
        var canAccess = await AuthorizationHelper.CanAccess(User, result.Data!.CustomerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own addresses");
        }

        return result.ToResult();
    }

    [HttpGet("customer/{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Addresses by Customer ID",
        Description =
            "This endpoint allows Admins and Customers to retrieve all addresses associated with a specific customer ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AddressDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<AddressDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAddressesByCustomerIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAddressResponseExample))]
    public async Task<IResult> GetAddressesByCustomerId(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can access only own addresses");
        }

        var query = new GetAddressesByCustomerIdQuery(customerId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Create New Address",
        Description = "This endpoint allows Admins and Customers to create a new address for a customer account."
    )]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAddressResponseExample))]
    public async Task<IResult> CreateAddress(
        [FromBody] CreateAddressCommand command,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, command.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can create addresses only for own account");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{addressId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Update Address",
        Description = "This endpoint allows Admins and Customers to update an existing address."
    )]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<AddressDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAddressResponseExample))]
    public async Task<IResult> UpdateAddress(
        string addressId,
        [FromBody] UpdateAddressCommand command,
        CancellationToken cancellationToken)
    {
        if (addressId != command.AddressId)
        {
            return ApiResponseResults.BadRequest("Address ID mismatch");
        }

        // First get the address to check ownership
        var query = new GetAddressByIdQuery(addressId);
        var addressResult = await mediator.Send(query, cancellationToken);

        if (!addressResult.IsSuccess || addressResult.Data == null)
        {
            return addressResult.ToResult();
        }

        var canAccess =
            await AuthorizationHelper.CanAccess(User, addressResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can update only own addresses");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{addressId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Delete Address",
        Description = "This endpoint allows Admins and Customers to delete an existing address."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteAddressResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedAddressResponseExample))]
    public async Task<IResult> DeleteAddress(
        string addressId,
        CancellationToken cancellationToken)
    {
        // First get the address to check ownership
        var query = new GetAddressByIdQuery(addressId);
        var addressResult = await mediator.Send(query, cancellationToken);

        if (!addressResult.IsSuccess || addressResult.Data == null)
        {
            return addressResult.ToResult();
        }

        var canAccess =
            await AuthorizationHelper.CanAccess(User, addressResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can delete only own addresses");
        }

        var command = new DeleteAddressCommand(addressId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}