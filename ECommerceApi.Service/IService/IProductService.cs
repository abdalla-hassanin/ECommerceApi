using ECommerceApi.Data.Entities;
using ECommerceApi.Service.Base;

namespace ECommerceApi.Service.IService;

public interface IProductService
{
    Task<Product?> GetProductByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default);
    Task<PaginationList<Product>> SearchProductsAsync(
        string? searchTerm, decimal? minPrice, decimal? maxPrice, 
        string? orderBy, bool isDescending, int currentPage , int pageSize, 
        CancellationToken cancellationToken = default);

    Task<PaginationList<Product>> GetProductsByCategoryAsync(
        string categoryId, string? orderBy, bool isDescending, int currentPage, int pageSize, 
        CancellationToken cancellationToken = default);

    Task<PaginationList<Product>> GetActiveProductsAsync(
        string? orderBy, bool isDescending, int currentPage, int pageSize, 
        CancellationToken cancellationToken = default);

    Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default);
    Task DeleteProductAsync(string id, CancellationToken cancellationToken = default);
}