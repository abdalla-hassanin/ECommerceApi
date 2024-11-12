namespace ECommerceApi.Data.Entities;

public class Review
{
    public string ReviewId { get; set; }= Ulid.NewUlid().ToString();
    public string ProductId { get; set; }
    public required Product Product { get; set; }
    public string CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public string? OrderId { get; set; }
    public Order? Order { get; set; } = null!;
    public required int Rating { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}