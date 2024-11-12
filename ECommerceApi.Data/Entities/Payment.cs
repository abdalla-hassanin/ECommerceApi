namespace ECommerceApi.Data.Entities;

public class Payment
{
    public string PaymentId { get; set;}= Ulid.NewUlid().ToString();
    public string OrderId { get; set; }
    public  Order Order { get; set; } = null!;
    public required string PaymentMethod { get; set; }
    public required decimal Amount { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? StripePaymentIntentId { get; set; }
    public string? StripeClientSecret { get; set; }
}