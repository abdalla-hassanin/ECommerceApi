using ECommerceApi.Data.Entities;
using ECommerceApi.Service.Base;

namespace ECommerceApi.Service.IService;

public interface IAuthService
{
    Task<AuthResult> RegisterUserAsync(ApplicationUser user, string password, string role, CancellationToken cancellationToken = default);
    Task<AuthResult> LoginAsync(string email, string password, CancellationToken cancellationToken = default);
    Task<AuthResult> RefreshTokenAsync(string accessToken, string refreshToken, CancellationToken cancellationToken = default);
    Task RevokeTokenAsync(string username, CancellationToken cancellationToken = default);
    Task<AuthResult> ForgotPasswordAsync(string email, CancellationToken cancellationToken = default);
    Task<AuthResult> ResetPasswordAsync(string email, string token, string newPassword, CancellationToken cancellationToken = default);
    Task<AuthResult> ChangePasswordAsync(string username, string currentPassword, string newPassword, CancellationToken cancellationToken = default);
    Task<AuthResult> ConfirmEmailAsync(string userId, string token, CancellationToken cancellationToken = default);
    Task<AuthResult> ResendEmailConfirmationAsync(string email, CancellationToken cancellationToken = default);
}