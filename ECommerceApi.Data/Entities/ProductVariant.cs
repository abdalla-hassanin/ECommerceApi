namespace ECommerceApi.Data.Entities;

public class ProductVariant
{
    public string VariantId { get; set; }= Ulid.NewUlid().ToString();
    public string ProductId { get; set; }
    public  Product Product { get; set; } = null!;
    public required string Name { get; set; }
    public required decimal Price { get; set; }
    public decimal? CompareAtPrice { get; set; }
    public decimal? CostPrice { get; set; }
    public required int InventoryQuantity { get; set; }
    public required int LowStockThreshold { get; set; }
    public decimal? Weight { get; set; }
    public string? WeightUnit { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public  ICollection<ProductImage> Images { get; set; } = null!;
}