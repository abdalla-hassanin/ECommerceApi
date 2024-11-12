using ECommerceApi.Data.Options;
using ECommerceApi.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;


namespace ECommerceApi.Service.Service;

public sealed class ImageProcessingService(
    ILogger<ImageProcessingService> logger,
    IOptions<ImageProcessingOptions> imageSettings)
    : IImageProcessingService
{
    private readonly ImageProcessingOptions _imageSettings = imageSettings.Value;

    public async Task<Stream> ResizeImageAsync(
        Stream imageStream,
        int? width = null,
        int? height = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var targetWidth = width ?? _imageSettings.DefaultWidth;
            var targetHeight = height ?? _imageSettings.DefaultHeight;
            
            var outputStream = new MemoryStream();
            using var image = await Image.LoadAsync(imageStream, cancellationToken);
            
            if (image.Width <= targetWidth && image.Height <= targetHeight)
            {
                await image.SaveAsJpegAsync(outputStream, cancellationToken: cancellationToken);
                outputStream.Position = 0;
                return outputStream;
            }

            image.Mutate(x => x.Resize(new ResizeOptions
            {
                Size = new Size(targetWidth, targetHeight),
                Mode = ResizeMode.Max,
                Sampler = KnownResamplers.Lanczos3
            }));

            await image.SaveAsJpegAsync(outputStream, cancellationToken: cancellationToken);
            outputStream.Position = 0;
            return outputStream;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to resize image");
            throw;
        }
    }

    public async Task<Stream> OptimizeImageAsync(
        Stream imageStream,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var outputStream = new MemoryStream();
            using var image = await Image.LoadAsync(imageStream, cancellationToken);
            
            await image.SaveAsWebpAsync(outputStream, new WebpEncoder
            {
                Quality = _imageSettings.DefaultQuality,
                FileFormat = WebpFileFormatType.Lossy,
                Method = WebpEncodingMethod.BestQuality
            }, cancellationToken);
            
            outputStream.Position = 0;
            return outputStream;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to optimize image");
            throw;
        }
    }
    public bool ValidateImage(IFormFile file)
    {
        try
        {
            long maxSizeInBytes = _imageSettings.MaxFileSizeInMb * 1024 * 1024;
            if (file.Length == 0 || file.Length > maxSizeInBytes)
            {
                logger.LogWarning("Invalid file size: {Size}", file.Length);
                return false;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!_imageSettings.AllowedExtensions.Contains(extension))
            {
                logger.LogWarning("Invalid file extension: {Extension}", extension);
                return false;
            }

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Image validation failed");
            return false;
        }
    }
}