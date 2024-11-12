using ECommerceApi.Data.Entities;
using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Service.IService;

public interface IProductImageService
{
    Task<ProductImage?> GetImageByIdAsync(string imageId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductImage>> GetAllImagesAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductImage>> GetImagesForProductAsync(string productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductImage>> GetImagesForVariantAsync(string variantId, CancellationToken cancellationToken = default);

    Task<ProductImage> CreateImageAsync(ProductImage image,IFormFile file, CancellationToken cancellationToken = default);
    Task<ProductImage> UpdateImageAsync(ProductImage image, IFormFile? file, CancellationToken cancellationToken = default);
    Task DeleteImageAsync(string imageId, CancellationToken cancellationToken = default);
}