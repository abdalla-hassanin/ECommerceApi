using Microsoft.AspNetCore.Http;

namespace ECommerceApi.Service.IService;

public interface IAwsStorageService
{
    Task<string> UploadImageAsync(IFormFile file,  string prefix, CancellationToken cancellationToken = default);
    Task<bool> DeleteImageAsync(string fileKey, CancellationToken cancellationToken = default);
    Task<string> GetImageAsync(string fileKey, CancellationToken cancellationToken = default);
}