namespace ECommerceApi.Core.MediatrHandlers.Address;

public record AddressDto(
    string AddressId,
    string CustomerId,
    string AddressLine,
    string City,
    string State,
    string Country,
    string? Phone,
    DateTime CreatedAt,
    DateTime UpdatedAt
);