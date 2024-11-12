namespace ECommerceApi.Core.MediatrHandlers.Customer;

public record CustomerDto
{
    public string CustomerId { get; init; }
    public string ApplicationUserId { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime? LastPurchaseDate { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Gender { get; init; }
    public string? Bio { get; init; }
    public string? ProfilePictureUrl { get; init; }
    public string? Language { get; init; }
}
