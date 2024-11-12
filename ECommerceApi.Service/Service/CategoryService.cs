using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryService> _logger;
    private readonly IAwsStorageService _awsStorageService;

    public CategoryService(IUnitOfWork unitOfWork, ILogger<CategoryService> logger,IAwsStorageService awsStorageService)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
        _awsStorageService = awsStorageService;
    }

    public async Task<Category?> GetCategoryByIdAsync(string categoryId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting category with ID: {CategoryId}", categoryId);
        return await _unitOfWork.Repository<Category>().GetByIdAsync(categoryId, cancellationToken);
    }

    public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all categories");
        return await _unitOfWork.Repository<Category>().ListAllAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Category>> GetCategoriesByParentIdAsync(string parentCategoryId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting categories by parent ID: {ParentCategoryId}", parentCategoryId);
        var spec = new CategorySpecifications.ByParentId(parentCategoryId);
        return await _unitOfWork.Repository<Category>().ListAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<Category>> GetActiveCategoriesAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting active categories");
        var spec = new CategorySpecifications.ActiveOnly();
        return await _unitOfWork.Repository<Category>().ListAsync(spec, cancellationToken);
    }

    public async Task<Category> CreateCategoryAsync(Category category, IFormFile file, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new category: {CategoryName}", category.Name);
        
        // Upload the image to storage
        var prefix = "category";
        category.ImageUrl = await _awsStorageService.UploadImageAsync(file, prefix, cancellationToken);

        await _unitOfWork.Repository<Category>().AddAsync(category, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Category created with ID: {CategoryId}", category.CategoryId);
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category,IFormFile? file, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating category with ID: {CategoryId}", category.CategoryId);
        if (file is not null)
        {
            // Delete the old image from storage
            await _awsStorageService.DeleteImageAsync(category.ImageUrl, cancellationToken);

            // Upload the new image to storage
            var prefix = "category";
            category.ImageUrl = await _awsStorageService.UploadImageAsync(file, prefix, cancellationToken);
        }
        await _unitOfWork.Repository<Category>().UpdateAsync(category, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Category updated successfully");
        return category;
    }

    public async Task DeleteCategoryAsync(string categoryId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting category with ID: {CategoryId}", categoryId);
        var category = await GetCategoryByIdAsync(categoryId, cancellationToken);
        if (category is not null)
        {
            // Delete the image from storage
            await _awsStorageService.DeleteImageAsync(category.ImageUrl, cancellationToken);

            await _unitOfWork.Repository<Category>().DeleteAsync(category, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Category deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent category with ID: {CategoryId}", categoryId);
        }
    }
}