using ECommerceApi.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Service.IService;

public interface ICategoryService
{
    Task<Category?> GetCategoryByIdAsync(string categoryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Category>> GetCategoriesByParentIdAsync(string parentCategoryId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default);

    Task<Category> CreateCategoryAsync(Category category,IFormFile file, CancellationToken cancellationToken = default);
    Task<Category> UpdateCategoryAsync(Category category,IFormFile? file, CancellationToken cancellationToken = default);
    Task DeleteCategoryAsync(string categoryId, CancellationToken cancellationToken = default);
    
}
