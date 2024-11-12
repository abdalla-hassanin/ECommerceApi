using Amazon.CloudFront;
using Amazon.CloudFront.Model;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using ECommerceApi.Data.Options;
using ECommerceApi.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ECommerceApi.Service.Service;
public sealed class AwsStorageService(
    IAmazonS3 s3Client,
    IAmazonCloudFront cloudFrontClient,
    IOptions<SecretOptions> secrets,
    IOptions<ImageProcessingOptions> imageSettings,
    IImageProcessingService imageProcessingService,
    ILogger<AwsStorageService> logger)
    : IAwsStorageService
{
    private readonly SecretOptions _secrets = secrets.Value;

    private readonly ImageProcessingOptions _imageSettings = imageSettings.Value;


    public async Task<string> UploadImageAsync(IFormFile file, string prefix, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Uploading image to S3 bucket: {BucketName}", _secrets.AWS.BucketName);
        try
        {
            // Validate image before processing
            if (!imageProcessingService.ValidateImage(file))
            {
                throw new ArgumentException("Invalid image file provided");
            }

            // Generate a unique file name with .webp extension since we're optimizing to WebP
            var fileName = $"{prefix}/{Guid.NewGuid()}.webp";

            await using var originalStream = file.OpenReadStream();
            
            // First resize the image
            await using var resizedStream = await imageProcessingService.ResizeImageAsync(
                originalStream,
                _imageSettings.DefaultWidth,
                _imageSettings.DefaultHeight,
                cancellationToken
            );

            // Then optimize the resized image
            await using var optimizedStream = await imageProcessingService.OptimizeImageAsync(
                resizedStream,
                cancellationToken
            );
            
            using var transferUtility = new TransferUtility(s3Client);
            
            // Configure the transfer request
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = optimizedStream,
                Key = fileName,
                BucketName = _secrets.AWS.BucketName,
                ContentType = "image/webp", // Always WebP after optimization
                CannedACL = S3CannedACL.Private
            };

            await transferUtility.UploadAsync(uploadRequest, cancellationToken);
            logger.LogInformation("Successfully uploaded image {fileName} to S3", fileName);
            
            
            return GetCloudFrontUrl(fileName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading image to S3");
            throw new ApplicationException("Failed to upload image to storage", ex);
        }
    }

    public async Task<bool> DeleteImageAsync(string fileKey, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Deleting image from S3 bucket: {BucketName}", _secrets.AWS.BucketName);
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _secrets.AWS.BucketName,
                Key = fileKey
            };

            await s3Client.DeleteObjectAsync(deleteRequest, cancellationToken);

            // Optionally invalidate CloudFront cache
            await InvalidateCloudFrontCache(fileKey);
            
            logger.LogInformation("Successfully deleted image {fileKey} from S3", fileKey);
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting image {fileKey} from S3", fileKey);
            return false;
        }
    }

    public async Task<string> GetImageAsync(string fileKey, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting image from S3 bucket: {BucketName}", _secrets.AWS.BucketName);
        try
        {
            var request = new GetObjectRequest
            {
                BucketName = _secrets.AWS.BucketName,
                Key = fileKey,
            };

            await s3Client.GetObjectAsync(request, cancellationToken);
            logger.LogInformation("Successfully retrieved image {fileKey} from S3", fileKey);
            
            return GetCloudFrontUrl(fileKey);
        } catch (AmazonS3Exception ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            logger.LogWarning("Image {fileKey} not found in S3", fileKey);
            return string.Empty;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting image {fileKey} from S3", fileKey);
            throw new ApplicationException("Failed to get image from storage", ex);
        }
    }
    private string GetCloudFrontUrl(string fileKey)
    {
        return $"{_secrets.AWS.CloudFrontDomain}/{fileKey}";
    }
    
    private async Task InvalidateCloudFrontCache(string fileKey)
    {
        try
        {
            var invalidationRequest = new CreateInvalidationRequest
            {
                DistributionId = _secrets.AWS.CloudFrontDomain,
                InvalidationBatch = new InvalidationBatch
                {
                    Paths = new Paths
                    {
                        Items = [$"/{fileKey}"],
                        Quantity = 1
                    },
                    CallerReference = DateTime.UtcNow.Ticks.ToString()
                }
            };

            await cloudFrontClient.CreateInvalidationAsync(invalidationRequest);
            logger.LogInformation("Successfully invalidated CloudFront cache for {FileKey}", fileKey);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error invalidating CloudFront cache for {FileKey}", fileKey);
        }
    }
}