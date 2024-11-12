using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class ProductImageService(
    IUnitOfWork unitOfWork,
    ILogger<ProductImageService> logger,
    IAwsStorageService awsStorageService)
    : IProductImageService
{
    public async Task<ProductImage?> GetImageByIdAsync(string imageId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting product image with ID: {ImageId}", imageId);
        return await unitOfWork.Repository<ProductImage>().GetByIdAsync(imageId.ToString(), cancellationToken);
    }

    public async Task<IReadOnlyList<ProductImage>> GetAllImagesAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting all product images");
        return await unitOfWork.Repository<ProductImage>().ListAllAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductImage>> GetImagesForProductAsync(string productId,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting images for product ID: {ProductId}", productId);
        var spec = new ProductImageSpecifications.ByProductId(productId);
        return await unitOfWork.Repository<ProductImage>().ListAsync(spec, cancellationToken);
    }

    public async Task<IReadOnlyList<ProductImage>> GetImagesForVariantAsync(string variantId,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting images for variant ID: {VariantId}", variantId);
        var spec = new ProductImageSpecifications.ByVariantId(variantId);
        return await unitOfWork.Repository<ProductImage>().ListAsync(spec, cancellationToken);
    }

    public async Task<ProductImage> CreateImageAsync(ProductImage image, IFormFile file,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Creating new product image for product ID: {ProductId}", image.ProductId);
        // Upload the image to storage
        var prefix = $"product/{image.ProductId}/variant/{image.VariantId}";
        image.ImageUrl = await awsStorageService.UploadImageAsync(file, prefix, cancellationToken);


        await unitOfWork.Repository<ProductImage>().AddAsync(image, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        logger.LogInformation("Product image created with ID: {ImageId}", image.ImageId);
        return image;
    }

    public async Task<ProductImage> UpdateImageAsync(ProductImage image, IFormFile? file,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Updating product image with ID: {ImageId}", image.ImageId);
        if (file is not null)
        {
            // Delete the old image from storage
            await awsStorageService.DeleteImageAsync(image.ImageUrl, cancellationToken);

            // Upload the new image to storage
            var prefix = $"product/{image.ProductId}/variant/{image.VariantId}";
            image.ImageUrl = await awsStorageService.UploadImageAsync(file, prefix, cancellationToken);
        }

        await unitOfWork.Repository<ProductImage>().UpdateAsync(image, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        logger.LogInformation("Product image updated successfully");
        return image;
    }

    public async Task DeleteImageAsync(string imageId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting product image with ID: {ImageId}", imageId);
        var image = await GetImageByIdAsync(imageId, cancellationToken);
        if (image is not null)
        {
            // Delete the image from storage
            await awsStorageService.DeleteImageAsync(image.ImageUrl, cancellationToken);

            // Delete the image from the database
            await unitOfWork.Repository<ProductImage>().DeleteAsync(image, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            logger.LogInformation("Product image deleted successfully");
        }
        else
        {
            logger.LogWarning("Attempted to delete non-existent product image with ID: {ImageId}", imageId);
        }
    }
}