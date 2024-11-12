using ECommerceApi.Api.Base;
using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Customer;
using ECommerceApi.Core.MediatrHandlers.Customer.Commands;
using ECommerceApi.Core.MediatrHandlers.Customer.Queries;
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
public class CustomerController(IMediator mediator, ICustomerService customerService, ILogger<CustomerController> logger) : ControllerBase
{
    [HttpGet("{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Customer by ID",
        Description = "This endpoint allows Admins and Customers to retrieve a customer by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetCustomerByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCustomerResponseExample))]
    public async Task<IResult> GetCustomerById(
        string customerId,
        CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(customerId);
        var result = await mediator.Send(query, cancellationToken);
        var canAccess = await AuthorizationHelper.CanAccess(User, result.Data!.CustomerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own Account");
        }

        return result.ToResult();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Get All Customers",
        Description = "This endpoint allows Admins to retrieve all customers."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CustomerDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CustomerDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllCustomersResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCustomerResponseExample))]
    public async Task<IResult> GetAllCustomers(CancellationToken cancellationToken)
    {
        var query = new GetAllCustomersQuery();
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }


    [HttpPut("{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Update Customer",
        Description = "This endpoint allows Admins and Customers to update a customer."
    )]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<CustomerDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCustomerResponseExample))]
    public async Task<IResult> UpdateCustomer(
        string customerId,
        [FromForm] UpdateCustomerCommand command,
        CancellationToken cancellationToken)
    {
        if (customerId != command.CustomerId)
        {
            return ApiResponseResults.BadRequest("Customer ID mismatch");
        }
       
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can update only own Account");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    
    [HttpDelete("{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Delete Customer",
        Description = "This endpoint allows Admins and Customers to delete a customer."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteCustomerResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedCustomerResponseExample))]
    public async Task<IResult> DeleteCustomer(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can delete only own account");
        }

        var command = new DeleteCustomerCommand(customerId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}