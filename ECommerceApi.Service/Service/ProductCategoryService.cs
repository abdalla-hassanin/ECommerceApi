using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class ProductCategoryService(IUnitOfWork unitOfWork, ILogger<ProductCategoryService> logger)
    : IProductCategoryService
{
    public async Task<IReadOnlyList<Category>> GetCategoriesForProductAsync(string productId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting categories for product ID: {ProductId}", productId);
        var spec = new ProductCategorySpecifications.CategoriesForProduct(productId);
        var productCategories = await unitOfWork.Repository<ProductCategory>().ListAsync(spec, cancellationToken);
        return productCategories.Select(pc => pc.Category).ToList();
    }

    public async Task<IReadOnlyList<Product>> GetProductsForCategoryAsync(string categoryId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting products for category ID: {CategoryId}", categoryId);
        var spec = new ProductCategorySpecifications.ProductsForCategory(categoryId);
        var productCategories = await unitOfWork.Repository<ProductCategory>().ListAsync(spec, cancellationToken);
        return productCategories.Select(pc => pc.Product).ToList();
    }

    public async Task AddProductToCategoryAsync(string productId, string categoryId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Adding product ID: {ProductId} to category ID: {CategoryId}", productId, categoryId);
        var spec = new ProductCategorySpecifications.ProductInCategory(productId, categoryId);
        var exists = (await unitOfWork.Repository<ProductCategory>().ListAsync(spec, cancellationToken)).Any();
        if (!exists)
        {
            var productCategory = new ProductCategory
            {
                ProductId = productId,
                CategoryId = categoryId
            };
            await unitOfWork.Repository<ProductCategory>().AddAsync(productCategory, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            logger.LogInformation("Product added to category successfully");
        }
        else
        {
            logger.LogInformation("Product is already in the specified category");
        }
    }

    public async Task RemoveProductFromCategoryAsync(string productId, string categoryId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Removing product ID: {ProductId} from category ID: {CategoryId}", productId, categoryId);
        var spec = new ProductCategorySpecifications.ProductInCategory(productId, categoryId);
        var productCategory = (await unitOfWork.Repository<ProductCategory>().ListAsync(spec, cancellationToken)).FirstOrDefault();
        if (productCategory is not null)
        {
            await unitOfWork.Repository<ProductCategory>().DeleteAsync(productCategory, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            logger.LogInformation("Product removed from category successfully");
        }
        else
        {
            logger.LogInformation("Product was not in the specified category");
        }
    }

    public async Task UpdateProductCategoriesAsync(string productId, IEnumerable<string> categoryIds, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating categories for product ID: {ProductId}", productId);
        var spec = new ProductCategorySpecifications.ForProduct(productId);
        var currentCategories = await unitOfWork.Repository<ProductCategory>().ListAsync(spec, cancellationToken);

        var categoriesToRemove = currentCategories.Where(pc => !categoryIds.Contains(pc.CategoryId));
        var categoriesToAdd = categoryIds.Where(cId => currentCategories.All(pc => pc.CategoryId != cId));

        foreach (var categoryToRemove in categoriesToRemove)
        {
            await unitOfWork.Repository<ProductCategory>().DeleteAsync(categoryToRemove, cancellationToken);
            logger.LogInformation("Removed product ID: {ProductId} from category ID: {CategoryId}", productId, categoryToRemove.CategoryId);
        }

        foreach (var categoryIdToAdd in categoriesToAdd)
        {
            await unitOfWork.Repository<ProductCategory>().AddAsync(new ProductCategory
            {
                ProductId = productId,
                CategoryId = categoryIdToAdd
            }, cancellationToken);
            logger.LogInformation("Added product ID: {ProductId} to category ID: {CategoryId}", productId, categoryIdToAdd);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        logger.LogInformation("Product categories updated successfully");
    }
}