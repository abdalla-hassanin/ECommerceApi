using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Order;
using Swashbuckle.AspNetCore.Filters;


namespace ECommerceApi.Api.ResponseExample;

public class GetOrderByIdResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-001",
            Status: "Processing",
            Subtotal: 100.00m,
            Tax: 10.00m,
            Shipping: 5.00m,
            Total: 115.00m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Standard",
            CouponId: null,
            Notes: "Please leave at the door",
            CreatedAt: DateTime.UtcNow.AddDays(-1),
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>
            {
                new OrderItemDto(
                    OrderItemId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId: null,
                    Quantity: 2,
                    Price: 50.00m,
                    Subtotal: 100.00m,
                    Tax: 10.00m,
                    Total: 110.00m,
                    CreatedAt: DateTime.UtcNow.AddDays(-1),
                    UpdatedAt: DateTime.UtcNow
                )
            }
        );

        return ApiResponse<OrderDto>.Factory.Success(orderDto);
    }
}

public class GetAllOrdersResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<OrderDto>>>
{
    public ApiResponse<IReadOnlyList<OrderDto>> GetExamples()
    {
        var orders = new List<OrderDto>
        {
            new OrderDto(
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderNumber: "ORD-2023-001",
                Status: "Processing",
                Subtotal: 100.00m,
                Tax: 10.00m,
                Shipping: 5.00m,
                Total: 115.00m,
                ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                PaymentMethod: "Credit Card",
                ShippingMethod: "Standard",
                CouponId: null,
                Notes: "Please leave at the door",
                CreatedAt: DateTime.UtcNow.AddDays(-1),
                UpdatedAt: DateTime.UtcNow,
                OrderItems: new List<OrderItemDto>()
            ),
            new OrderDto(
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderNumber: "ORD-2023-002",
                Status: "Shipped",
                Subtotal: 200.00m,
                Tax: 20.00m,
                Shipping: 10.00m,
                Total: 230.00m,
                ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                PaymentMethod: "PayPal",
                ShippingMethod: "Express",
                CouponId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                Notes: null,
                CreatedAt: DateTime.UtcNow.AddDays(-2),
                UpdatedAt: DateTime.UtcNow,
                OrderItems: new List<OrderItemDto>()
            )
        };

        return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(orders);
    }
}

public class GetOrdersByCustomerIdResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<OrderDto>>>
{
    public ApiResponse<IReadOnlyList<OrderDto>> GetExamples()
    {
        var orders = new List<OrderDto>
        {
            new OrderDto(
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderNumber: "ORD-2023-001",
                Status: "Processing",
                Subtotal: 100.00m,
                Tax: 10.00m,
                Shipping: 5.00m,
                Total: 115.00m,
                ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                PaymentMethod: "Credit Card",
                ShippingMethod: "Standard",
                CouponId: null,
                Notes: "Please leave at the door",
                CreatedAt: DateTime.UtcNow.AddDays(-1),
                UpdatedAt: DateTime.UtcNow,
                OrderItems: new List<OrderItemDto>()
            )
        };

        return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(orders);
    }
}

public class GetOrdersByStatusResponseExample : IExamplesProvider<ApiResponse<IReadOnlyList<OrderDto>>>
{
    public ApiResponse<IReadOnlyList<OrderDto>> GetExamples()
    {
        var orders = new List<OrderDto>
        {
            new OrderDto(
                OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                OrderNumber: "ORD-2023-001",
                Status: "Processing",
                Subtotal: 100.00m,
                Tax: 10.00m,
                Shipping: 5.00m,
                Total: 115.00m,
                ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                PaymentMethod: "Credit Card",
                ShippingMethod: "Standard",
                CouponId: null,
                Notes: "Please leave at the door",
                CreatedAt: DateTime.UtcNow.AddDays(-1),
                UpdatedAt: DateTime.UtcNow,
                OrderItems: new List<OrderItemDto>()
            )
        };

        return ApiResponse<IReadOnlyList<OrderDto>>.Factory.Success(orders);
    }
}

public class CreatedOrderResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-003",
            Status: "Pending",
            Subtotal: 150.00m,
            Tax: 15.00m,
            Shipping: 7.50m,
            Total: 172.50m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Standard",
            CouponId: null,
            Notes: null,
            CreatedAt: DateTime.UtcNow,
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>()
        );

        return ApiResponse<OrderDto>.Factory.Created(orderDto, "Order created successfully");
    }
}

public class UpdateOrderResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-001",
            Status: "Shipped",
            Subtotal: 100.00m,
            Tax: 10.00m,
            Shipping: 5.00m,
            Total: 115.00m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Express",
            CouponId: null,
            Notes: "Please leave at the door",
            CreatedAt: DateTime.UtcNow.AddDays(-1),
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>()
        );

        return ApiResponse<OrderDto>.Factory.Success(orderDto, "Order updated successfully");
    }
}

public class DeleteOrderResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Order deleted successfully");
    }
}

public class BadRequestOrderResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        return ApiResponse<OrderDto>.Factory.BadRequest(
            "Invalid order data",
            new List<string> 
            { 
                "Customer ID is required.",
                "Order number is required.",
                "Total must be greater than 0."
            }
        );
    }
}

public class UnauthorizedOrderResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        return ApiResponse<OrderDto>.Factory.Unauthorized("Unauthorized access");
    }
}

public class NotFoundOrderResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        return ApiResponse<OrderDto>.Factory.NotFound("Order not found");
    }
}
public class AddOrderItemResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-001",
            Status: "Processing",
            Subtotal: 150.00m,
            Tax: 15.00m,
            Shipping: 5.00m,
            Total: 170.00m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Standard",
            CouponId: null,
            Notes: "Please leave at the door",
            CreatedAt: DateTime.UtcNow.AddDays(-1),
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>
            {
                new OrderItemDto(
                    OrderItemId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId: null,
                    Quantity: 2,
                    Price: 50.00m,
                    Subtotal: 100.00m,
                    Tax: 10.00m,
                    Total: 110.00m,
                    CreatedAt: DateTime.UtcNow.AddDays(-1),
                    UpdatedAt: DateTime.UtcNow
                ),
                new OrderItemDto(
                    OrderItemId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId: null,
                    Quantity: 1,
                    Price: 50.00m,
                    Subtotal: 50.00m,
                    Tax: 5.00m,
                    Total: 55.00m,
                    CreatedAt: DateTime.UtcNow,
                    UpdatedAt: DateTime.UtcNow
                )
            }
        );

        return ApiResponse<OrderDto>.Factory.Created(orderDto, "Order item added successfully");
    }
}

public class UpdateOrderItemResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-001",
            Status: "Processing",
            Subtotal: 200.00m,
            Tax: 20.00m,
            Shipping: 5.00m,
            Total: 225.00m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Standard",
            CouponId: null,
            Notes: "Please leave at the door",
            CreatedAt: DateTime.UtcNow.AddDays(-1),
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>
            {
                new OrderItemDto(
                    OrderItemId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId: null,
                    Quantity: 4,
                    Price: 50.00m,
                    Subtotal: 200.00m,
                    Tax: 20.00m,
                    Total: 220.00m,
                    CreatedAt: DateTime.UtcNow.AddDays(-1),
                    UpdatedAt: DateTime.UtcNow
                )
            }
        );

        return ApiResponse<OrderDto>.Factory.Success(orderDto, "Order item updated successfully");
    }
}

public class DeleteOrderItemResponseExample : IExamplesProvider<ApiResponse<OrderDto>>
{
    public ApiResponse<OrderDto> GetExamples()
    {
        var orderDto = new OrderDto(
            OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            CustomerId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            OrderNumber: "ORD-2023-001",
            Status: "Processing",
            Subtotal: 50.00m,
            Tax: 5.00m,
            Shipping: 5.00m,
            Total: 60.00m,
            ShippingAddressId: "01HF3WFKX1KPY89WNJRXJ6V18N",
            PaymentMethod: "Credit Card",
            ShippingMethod: "Standard",
            CouponId: null,
            Notes: "Please leave at the door",
            CreatedAt: DateTime.UtcNow.AddDays(-1),
            UpdatedAt: DateTime.UtcNow,
            OrderItems: new List<OrderItemDto>
            {
                new OrderItemDto(
                    OrderItemId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    OrderId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    ProductId: "01HF3WFKX1KPY89WNJRXJ6V18N",
                    VariantId: null,
                    Quantity: 1,
                    Price: 50.00m,
                    Subtotal: 50.00m,
                    Tax: 5.00m,
                    Total: 55.00m,
                    CreatedAt: DateTime.UtcNow,
                    UpdatedAt: DateTime.UtcNow
                )
            }
        );

        return ApiResponse<OrderDto>.Factory.Success(orderDto, "Order item deleted successfully");
    }
}