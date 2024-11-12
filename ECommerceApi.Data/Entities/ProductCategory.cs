namespace ECommerceApi.Data.Entities;

public class ProductCategory
{
    public string ProductId { get; set; }
    public  Product Product { get; set; } = null!;
    public string CategoryId { get; set; }
    public  Category Category { get; set; } = null!;
}