using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Service.IService;

public interface IImageProcessingService
{
    Task<Stream> ResizeImageAsync(Stream imageStream, int? width = null, int? height = null, CancellationToken cancellationToken = default);
    Task<Stream> OptimizeImageAsync(Stream imageStream, CancellationToken cancellationToken = default);
    bool ValidateImage(IFormFile file);
}
