namespace ECommerceApi.Core.MediatrHandlers.Admin;

public record AdminDto(
    string AdminId,
    string ApplicationUserId,
    string Position,
    string FirstName,
    string LastName,
    string Email
);