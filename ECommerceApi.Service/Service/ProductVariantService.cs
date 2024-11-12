using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class ProductVariantService : IProductVariantService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductVariantService> _logger;

    public ProductVariantService(IUnitOfWork unitOfWork, ILogger<ProductVariantService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<ProductVariant?> GetVariantByIdAsync(string variantId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting product variant with ID: {VariantId}", variantId);
        var spec = new ProductVariantSpecifications.ByVariantId(variantId);
        var productVariants = await _unitOfWork.Repository<ProductVariant>().ListAsync(spec, cancellationToken);
        return productVariants.FirstOrDefault();
    }

    public async Task<IReadOnlyList<ProductVariant>> GetVariantsForProductAsync(string productId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting variants for product ID: {ProductId}", productId);
        var spec = new ProductVariantSpecifications.ByProductId(productId);
        return await _unitOfWork.Repository<ProductVariant>().ListAsync(spec, cancellationToken);
    }

    public async Task<ProductVariant> CreateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new product variant for product ID: {ProductId}", variant.ProductId);
        await _unitOfWork.Repository<ProductVariant>().AddAsync(variant, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Product variant created with ID: {VariantId}", variant.VariantId);
        return variant;
    }

    public async Task<ProductVariant> UpdateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating product variant with ID: {VariantId}", variant.VariantId);
        await _unitOfWork.Repository<ProductVariant>().UpdateAsync(variant, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Product variant updated successfully");
        return variant;
    }

    public async Task DeleteVariantAsync(string variantId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting product variant with ID: {VariantId}", variantId);
        var variant = await GetVariantByIdAsync(variantId, cancellationToken);
        if (variant is not null)
        {
            await _unitOfWork.Repository<ProductVariant>().DeleteAsync(variant, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Product variant deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent product variant with ID: {VariantId}", variantId);
        }
    }
}