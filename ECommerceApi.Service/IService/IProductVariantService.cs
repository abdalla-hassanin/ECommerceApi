using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IProductVariantService
{
    Task<ProductVariant?> GetVariantByIdAsync(string variantId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductVariant>> GetVariantsForProductAsync(string productId, CancellationToken cancellationToken = default);
    Task<ProductVariant> CreateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default);
    Task<ProductVariant> UpdateVariantAsync(ProductVariant variant, CancellationToken cancellationToken = default);
    Task DeleteVariantAsync(string variantId, CancellationToken cancellationToken = default);
}
