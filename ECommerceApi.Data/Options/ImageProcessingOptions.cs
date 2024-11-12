using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Data.Options;

public sealed class ImageProcessingOptions
{
    public const string SectionName = "ImageProcessing";
    
    [Required]
    [Range(1, 100)]
    public required int MaxFileSizeInMb { get; init; }
    
    [Required]
    [Range(1, 100)]
    public required int DefaultQuality { get; init; }
    
    [Required]
    public required string[] AllowedExtensions { get; init; }
    
    [Required]
    [Range(1, 10000)]
    public required int DefaultWidth { get; init; }
    
    [Required]
    [Range(1, 10000)]
    public required int DefaultHeight { get; init; }
}