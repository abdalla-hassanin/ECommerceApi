namespace ECommerceApi.Data.Entities;

public class Wishlist
{
    public string WishlistId { get; set; }= Ulid.NewUlid().ToString();
    public string CustomerId { get; set; }
    public required Customer Customer { get; set; }
    public string ProductId { get; set; }
    public required Product Product { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
}