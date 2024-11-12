using ECommerceApi.Api.Base;
using ECommerceApi.Api.ResponseExample;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Order;
using ECommerceApi.Core.MediatrHandlers.Order.Commands;
using ECommerceApi.Core.MediatrHandlers.Order.Queries;
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
public class OrderController(IMediator mediator,ICustomerService customerService, ILogger<OrderController> logger) : ControllerBase
{
    [HttpGet("{orderId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Order by ID",
        Description = "This endpoint allows Admins and Customers to retrieve an order by its ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOrderByIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> GetOrderById(
        string orderId,
        CancellationToken cancellationToken)
    {
        
        var query = new GetOrderByIdQuery(orderId);
        var result = await mediator.Send(query, cancellationToken);
        var canAccess = await AuthorizationHelper.CanAccess(User, result.Data!.CustomerId, customerService, logger);
        if (canAccess == false)
        {
            return ApiResponseResults.Unauthorized("Can access only own orders");
        }

        return result.ToResult();
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Get All Orders",
        Description = "This endpoint allows Admins to retrieve all orders."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetAllOrdersResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> GetAllOrders(CancellationToken cancellationToken)
    {
        var query = new GetAllOrdersQuery();
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("customer/{customerId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Get Orders by Customer ID",
        Description = "This endpoint allows Admins and Customers to retrieve all orders associated with a specific customer ID."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOrdersByCustomerIdResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> GetOrdersByCustomerId(
        string customerId,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, customerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can access only own orders");
        }
   
        var query = new GetOrdersByCustomerIdQuery(customerId);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpGet("status/{status}")]
    [Authorize(Roles = "Admin")]
    [SwaggerOperation(
        Summary = "Get Orders by Status",
        Description = "This endpoint allows Admins to retrieve all orders associated with a specific status."
    )]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<OrderDto>>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(GetOrdersByStatusResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> GetOrdersByStatus(
        string status,
        CancellationToken cancellationToken)
    {
        var query = new GetOrdersByStatusQuery(status);
        var result = await mediator.Send(query, cancellationToken);
        return result.ToResult();
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Create New Order",
        Description = "This endpoint allows Admins and Customers to create a new order."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(CreatedOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> CreateOrder(
        [FromBody] CreateOrderCommand command,
        CancellationToken cancellationToken)
    {
        var canAccess = await AuthorizationHelper.CanAccess(User, command.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can create orders only for own account");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{orderId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Update Order",
        Description = "This endpoint allows Admins and Customers to update an order."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> UpdateOrder(
        string orderId,
        [FromBody] UpdateOrderCommand command,
        CancellationToken cancellationToken)
    {
        if (orderId != command.OrderId)
        {
            return ApiResponseResults.BadRequest("Order ID mismatch");
        }

        var query = new GetOrderByIdQuery(orderId);
        var orderResult = await mediator.Send(query, cancellationToken);
        
        if (!orderResult.IsSuccess || orderResult.Data == null)
        {
            return orderResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, orderResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can update only own orders");
        }
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{orderId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Delete Order",
        Description = "This endpoint allows Admins and Customers to delete an order."
    )]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<bool>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> DeleteOrder(
        string orderId,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(orderId);
        var orderResult = await mediator.Send(query, cancellationToken);
        
        if (!orderResult.IsSuccess || orderResult.Data == null)
        {
            return orderResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, orderResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can delete only own orders");
        }

        var command = new DeleteOrderCommand(orderId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPost("{orderId}/items")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Add Order Item",
        Description = "This endpoint allows Admins and Customers to add an item to an order."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status201Created, typeof(AddOrderItemResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> AddOrderItem(
        string orderId,
        [FromBody] AddOrderItemCommand command,
        CancellationToken cancellationToken)
    {
        if (orderId != command.OrderId)
        {
            return ApiResponseResults.BadRequest("Order ID mismatch");
        }
        var query = new GetOrderByIdQuery(orderId);
        var orderResult = await mediator.Send(query, cancellationToken);
        
        if (!orderResult.IsSuccess || orderResult.Data == null)
        {
            return orderResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, orderResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can add order Items only own orders");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpPut("{orderId}/items/{orderItemId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Update Order Item",
        Description = "This endpoint allows Admins and Customers to update an item in an order."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(UpdateOrderItemResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status400BadRequest, typeof(BadRequestOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> UpdateOrderItem(
        string orderId,
        string orderItemId,
        [FromBody] UpdateOrderItemCommand command,
        CancellationToken cancellationToken)
    {
        if (orderId != command.OrderId || orderItemId != command.OrderItemId)
        {
            return ApiResponseResults.BadRequest("Order ID or Order Item ID mismatch");
        }
        var query = new GetOrderByIdQuery(orderId);
        var orderResult = await mediator.Send(query, cancellationToken);
        
        if (!orderResult.IsSuccess || orderResult.Data == null)
        {
            return orderResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, orderResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can update order Items only own orders");
        }

        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }

    [HttpDelete("{orderId}/items/{orderItemId}")]
    [Authorize(Roles = "Admin,Customer")]
    [SwaggerOperation(
        Summary = "Delete Order Item",
        Description = "This endpoint allows Admins and Customers to delete an item from an order."
    )]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<OrderDto>), StatusCodes.Status401Unauthorized)]
    [SwaggerResponseExample(StatusCodes.Status200OK, typeof(DeleteOrderItemResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status404NotFound, typeof(NotFoundOrderResponseExample))]
    [SwaggerResponseExample(StatusCodes.Status401Unauthorized, typeof(UnauthorizedOrderResponseExample))]
    public async Task<IResult> DeleteOrderItem(
        string orderId,
        string orderItemId,
        CancellationToken cancellationToken)
    {
        var query = new GetOrderByIdQuery(orderId);
        var orderResult = await mediator.Send(query, cancellationToken);
        
        if (!orderResult.IsSuccess || orderResult.Data == null)
        {
            return orderResult.ToResult();
        }

        var canAccess = await AuthorizationHelper.CanAccess(User, orderResult.Data.CustomerId, customerService, logger);
        if (!canAccess)
        {
            return ApiResponseResults.Unauthorized("Can delete order Items only own orders");
        }

        var command = new DeleteOrderItemCommand(orderId, orderItemId);
        var result = await mediator.Send(command, cancellationToken);
        return result.ToResult();
    }
}