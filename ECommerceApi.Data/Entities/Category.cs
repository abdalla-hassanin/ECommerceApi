namespace ECommerceApi.Data.Entities;

public class Category
{
    public string CategoryId { get; set; }= Ulid.NewUlid().ToString();
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string ImageUrl { get; set; }
    public required bool IsActive { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> Subcategories { get; set; } = null!;
    public  ICollection<ProductCategory> ProductCategories { get; set; } = null!;
}