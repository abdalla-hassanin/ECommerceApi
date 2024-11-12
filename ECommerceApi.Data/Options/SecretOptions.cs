using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Data.Options;

public sealed class SecretOptions
{
    public const string SectionName = "Secrets"; 
    
    public string AppUrl { get; init; } = null!;

    public DatabaseSecrets ConnectionStrings { get; set; } = null!;
    public EmailSecrets EmailSettings { get; set; } = null!;
    public JwtSecrets JWT { get; set; } = null!;
    public AwsSecrets AWS { get; set; } = null!;
}

public sealed class DatabaseSecrets
{
    [Required]
    public string DefaultConnection  { get; init; } = null!;
}

public sealed class EmailSecrets
{
    [Required]
    public string Host { get; init; } = null!;
    
    [Required]
    [Range(1, 65535)]
    public int Port { get; init; }
    
    [Required]
    [EmailAddress]
    public string UserName { get; init; } = null!;
    
    [Required]
    public string Password { get; init; } = null!;
}

public sealed class JwtSecrets
{
    [Required]
    [MinLength(32)]
    public string Key { get; init; } = null!;
    
    [Required]
    public string Issuer { get; init; } = null!;
    
    [Required]
    public string Audience { get; init; } = null!;
    
    [Required]
    [Range(1, 1440)]
    public int AccessTokenExpirationMinutes { get; init; }
}

public sealed class AwsSecrets
{
    [Required]
    public string AccessKey { get; init; } = null!;
    
    [Required]
    public string SecretKey { get; init; } = null!;
    
    [Required]
    public string BucketName { get; init; } = null!;
    
    [Required]
    public string Region { get; init; } = null!;
    
    [Required]
    [Url]
    public string CloudFrontDomain { get; init; } = null!;
}

