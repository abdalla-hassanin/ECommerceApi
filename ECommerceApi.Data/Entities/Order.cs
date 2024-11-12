namespace ECommerceApi.Data.Entities;

public class Order
{
    public  string OrderId { get; set; }= Ulid.NewUlid().ToString();
    public  string CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public required string OrderNumber { get; set; }
    public required string Status { get; set; }
    public required decimal Subtotal { get; set; }
    public required decimal Tax { get; set; }
    public required decimal Shipping { get; set; }
    public required decimal Total { get; set; }
    public  string ShippingAddressId { get; set; }
    public required Address ShippingAddress { get; set; }
    public required string PaymentMethod { get; set; }
    public required string ShippingMethod { get; set; }
    public string? CouponId { get; set; }
    public Coupon? Coupon { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public required ICollection<OrderItem> OrderItems { get; set; }
    public Payment? Payment { get; set; }
}