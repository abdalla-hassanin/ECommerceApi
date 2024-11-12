using System.Security.Claims;
using ECommerceApi.Service.IService;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace ECommerceApi.Api.Base;

public static class AuthorizationHelper
{
    public static async Task<bool> CanAccess(ClaimsPrincipal user, string customerId, ICustomerService customerService,
        ILogger logger)
    {
        // If user is Admin, they can access everything
        if (user.IsInRole("Admin"))
        {
            logger.LogInformation("User is admin, access granted");
            return true;
        }

        // Get the user ID using the same claim type used in generation
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        logger.LogInformation("Authorization attempt - Found claims:");
        foreach (var claim in user.Claims)
        {
            logger.LogInformation("Claim: {Type} = {Value}", claim.Type, claim.Value);
        }

        logger.LogInformation("Attempting authorization - User ID: {UserId}, Target Customer ID: {CustomerId}",
            userId, customerId);

        if (string.IsNullOrEmpty(userId))
        {
            logger.LogWarning("Authorization failed - User ID claim not found. Available claims: {@Claims}",
                user.Claims.Select(c => new { c.Type, c.Value }));
            return false;
        }

        // Get customer associated with the user
        var customer = await customerService.GetCustomerByUserIdAsync(userId);

        if (customer == null)
        {
            logger.LogWarning("Authorization failed - No customer found for User ID: {UserId}", userId);
            return false;
        }

        var hasAccess = customer.CustomerId == customerId;
        logger.LogInformation(
            "Authorization {Result} - User ID: {UserId}, Customer ID: {CustomerID}, Target Customer ID: {TargetCustomerId}",
            hasAccess ? "granted" : "denied",
            userId,
            customer.CustomerId,
            customerId);

        return hasAccess;
    }
}