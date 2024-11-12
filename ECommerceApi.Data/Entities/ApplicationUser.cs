using Microsoft.AspNetCore.Identity;

namespace ECommerceApi.Data.Entities;

public sealed class ApplicationUser : IdentityUser
{
    public ApplicationUser()
    {
        Id = Ulid.NewUlid().ToString();
    }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public Customer? Customer { get; set; }
    public Admin? Admin { get; set; }
}