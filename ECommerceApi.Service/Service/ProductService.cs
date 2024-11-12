using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class ProductService(IUnitOfWork unitOfWork, ILogger<ProductService> logger) : IProductService
{
    public async Task<Product?> GetProductByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting product with ID: {ProductId}", id);
        return await unitOfWork.Repository<Product>().GetByIdAsync(id.ToString(), cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting all products");
        return await unitOfWork.Repository<Product>().ListAllAsync(cancellationToken);
    }

    public async Task<PaginationList<Product>> SearchProductsAsync(
        string? searchTerm, decimal? minPrice, decimal? maxPrice, 
        string? orderBy, bool isDescending, int currentPage, int pageSize, 
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Searching products with parameters: searchTerm={SearchTerm}, minPrice={MinPrice}, maxPrice={MaxPrice}, orderBy={OrderBy}, isDescending={IsDescending}, currentPage={CurrentPage}, pageSize={PageSize}", 
            searchTerm, minPrice, maxPrice, orderBy, isDescending, currentPage, pageSize);

        var countSpec = new ProductSpecifications.SearchCount(searchTerm, minPrice, maxPrice);
        var totalCount = (await unitOfWork.Repository<Product>().ListAsync(countSpec, cancellationToken)).Count;

        var spec = new ProductSpecifications.Search(searchTerm, minPrice, maxPrice, orderBy, isDescending, currentPage, pageSize);
        var products = await unitOfWork.Repository<Product>().ListAsync(spec, cancellationToken);

        logger.LogInformation("Found {ProductCount} products matching search criteria", products.Count);

        return new PaginationList<Product>
        {
            Items = products,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<PaginationList<Product>> GetProductsByCategoryAsync(
        string categoryId, string? orderBy, bool isDescending, int currentPage, int pageSize, 
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting products for category ID: {CategoryId}, orderBy={OrderBy}, isDescending={IsDescending}, currentPage={CurrentPage}, pageSize={PageSize}", 
            categoryId, orderBy, isDescending, currentPage, pageSize);

        var countSpec = new ProductSpecifications.ByCategoryCount(categoryId);
        var totalCount = (await unitOfWork.Repository<Product>().ListAsync(countSpec, cancellationToken)).Count;

        var spec = new ProductSpecifications.ByCategory(categoryId, orderBy, isDescending, currentPage, pageSize);
        var products = await unitOfWork.Repository<Product>().ListAsync(spec, cancellationToken);

        logger.LogInformation("Found {ProductCount} products in category {CategoryId}", products.Count, categoryId);

        return new PaginationList<Product>
        {
            Items = products,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<PaginationList<Product>> GetActiveProductsAsync(
        string? orderBy, bool isDescending, int currentPage, int pageSize, 
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting active products: orderBy={OrderBy}, isDescending={IsDescending}, currentPage={CurrentPage}, pageSize={PageSize}", 
            orderBy, isDescending, currentPage, pageSize);

        var countSpec = new ProductSpecifications.ActiveOnlyCount();
        var totalCount = (await unitOfWork.Repository<Product>().ListAsync(countSpec, cancellationToken)).Count;

        var spec = new ProductSpecifications.ActiveOnly(orderBy, isDescending, currentPage, pageSize);
        var products = await unitOfWork.Repository<Product>().ListAsync(spec, cancellationToken);

        logger.LogInformation("Found {ProductCount} active products", products.Count);

        return new PaginationList<Product>
        {
            Items = products,
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
        };
    }

    public async Task<Product> CreateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating new product: {ProductName}", product.Name);
        await unitOfWork.Repository<Product>().AddAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        logger.LogInformation("Product created successfully with ID: {ProductId}", product.ProductId);
        return product;
    }

    public async Task<Product> UpdateProductAsync(Product product, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating product with ID: {ProductId}", product.ProductId);
        await unitOfWork.Repository<Product>().UpdateAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        logger.LogInformation("Product updated successfully");
        return product;
    }

    public async Task DeleteProductAsync(string id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting product with ID: {ProductId}", id);
        var product = await GetProductByIdAsync(id, cancellationToken);
        if (product is not null)
        {
            await unitOfWork.Repository<Product>().DeleteAsync(product, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            logger.LogInformation("Product deleted successfully");
        }
        else
        {
            logger.LogWarning("Attempted to delete non-existent product with ID: {ProductId}", id);
        }
    }
}