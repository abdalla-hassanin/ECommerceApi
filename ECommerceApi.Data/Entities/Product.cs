namespace ECommerceApi.Data.Entities;

public class Product
{
    public string ProductId { get; set; }= Ulid.NewUlid().ToString();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ShortDescription { get; set; }
    public required decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public required string Status { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public  ICollection<ProductCategory> ProductCategories { get; set; } = null!;
    public ICollection<ProductVariant> Variants { get; set; } = null!;
    public  ICollection<ProductImage> Images { get; set; } = null!;
    public ICollection<Review> Reviews { get; set; } = null!;
}