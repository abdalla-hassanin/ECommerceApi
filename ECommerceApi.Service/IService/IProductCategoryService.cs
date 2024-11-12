using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IProductCategoryService
{
    Task<IReadOnlyList<Category>> GetCategoriesForProductAsync(string productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetProductsForCategoryAsync(string categoryId, CancellationToken cancellationToken = default);
    Task AddProductToCategoryAsync(string productId, string categoryId, CancellationToken cancellationToken = default);
    Task RemoveProductFromCategoryAsync(string productId, string categoryId, CancellationToken cancellationToken = default);
    Task UpdateProductCategoriesAsync(string productId, IEnumerable<string> categoryIds, CancellationToken cancellationToken = default);
}