namespace ECommerceApi.Service.Base;

public sealed record AuthResult
{
    public required bool Succeeded { get; init; }
    public string? Message { get; init; }
    public string? Role { get; set; }
    public string? UserId { get; set; }
    public string? Token { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? TokenExpiration { get; init; }
}