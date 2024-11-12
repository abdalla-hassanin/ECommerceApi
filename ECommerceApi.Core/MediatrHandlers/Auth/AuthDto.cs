namespace ECommerceApi.Core.MediatrHandlers.Auth;

public record AuthDto(
    string? Role,
    string? Id,
    string? Token,
    string? RefreshToken,
    DateTime? TokenExpiration
)
{
    public AuthDto() : this(null, null, null, null, null)
    {
    }
}