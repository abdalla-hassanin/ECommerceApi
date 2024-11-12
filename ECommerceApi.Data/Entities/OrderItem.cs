namespace ECommerceApi.Data.Entities;

public class OrderItem
{
    public string OrderItemId { get; set; }= Ulid.NewUlid().ToString();
    public  string OrderId { get; set; }
    public required Order Order { get; set; }
    public string ProductId { get; set; }
    public required Product Product { get; set; }
    public string? VariantId { get; set; }
    public ProductVariant? Variant { get; set; }
    public required int Quantity { get; set; }
    public required decimal Price { get; set; }
    public required decimal Subtotal { get; set; }
    public required decimal Tax { get; set; }
    public required decimal Total { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}