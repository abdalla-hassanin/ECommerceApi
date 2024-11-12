namespace ECommerceApi.Data.Entities;

public class Customer
{
    public  string CustomerId { get; set; }= Ulid.NewUlid().ToString();
    public  string ApplicationUserId { get; set; }
    public  ApplicationUser ApplicationUser { get; set; } = null!;
    public DateTime? LastPurchaseDate { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Gender { get; set; }
    public string? Bio { get; set; }
    public required string ProfilePictureUrl { get; set; }
    public string? Language { get; set; }
    public  ICollection<Address> Addresses { get; set; } = null!;
    public  ICollection<Order> Orders { get; set; } = null!;
    public  ICollection<Review> Reviews { get; set; } = null!;
    public  ICollection<Wishlist> WishlistItems { get; set; } = null!;
}