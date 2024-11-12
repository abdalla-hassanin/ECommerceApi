using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using ECommerceApi.Data.Entities;
using ECommerceApi.Data.Options;
using ECommerceApi.Service.Base;
using ECommerceApi.Service.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ECommerceApi.Service.Service;

public sealed class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IEmailService _emailService;
    private readonly SecretOptions _secretOptions;
    private readonly ILogger<AuthService> _logger;
    private const int RefreshTokenExpiryDays = 7;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        ITokenService tokenService,
        IEmailService emailService,
        IOptions<SecretOptions> secretOptions,
        ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _emailService = emailService;
        _secretOptions = secretOptions.Value;
        _logger = logger;
    }

    public async Task<AuthResult> RegisterUserAsync(
        ApplicationUser user,
        string password,
        string role,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(user);
        ArgumentException.ThrowIfNullOrEmpty(password);
        ArgumentException.ThrowIfNullOrEmpty(role);

        _logger.LogInformation("Attempting to register user with email: {Email}", user.Email);

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            _logger.LogWarning("User registration failed for email: {Email}. Errors: {Errors}",
                user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return new AuthResult
            {
                Succeeded = false,
                Message = $"Registration failed. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}"
            };
        }

        await _userManager.AddToRoleAsync(user, role);

        _logger.LogInformation("User registered successfully with email: {Email} and role: {Role}", user.Email, role);

        var (_, callbackUrl) = await GenerateEmailConfirmationTokenAndUrl(user);

        await _emailService.SendEmailAsync(
            user.Email!,
            "Confirm your email",
            GenerateEmailConfirmationMessage(callbackUrl));

        _logger.LogInformation("Confirmation email sent to: {Email}", user.Email);

        return new AuthResult
        {
            Succeeded = true,
            Message = $"{role} registered successfully. Please check your email to confirm your account."
        };
    }

    public async Task<AuthResult> LoginAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(email);
        ArgumentException.ThrowIfNullOrEmpty(password);

        _logger.LogInformation("Login attempt for user: {Email}", email);

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null || !await _userManager.CheckPasswordAsync(user, password))
        {
            _logger.LogWarning("Failed login attempt for user: {Email}", email);
            return new AuthResult { Succeeded = false, Message = "Invalid email or password" };
        }

        var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        var authClaims = await GenerateAuthClaims(user, userRole);
        var token = _tokenService.GenerateAccessToken(authClaims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        var tokenExpiration = DateTime.UtcNow.AddMinutes(_tokenService.GetAccessTokenExpirationMinutes());

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("User {Email} logged in successfully with role: {Role}", email, userRole);

        return new AuthResult
        {
            Succeeded = true,
            Role = userRole,
            UserId = user.Id,
            Token = token,
            RefreshToken = refreshToken,
            TokenExpiration = tokenExpiration,
            Message = "Login successful"
        };
    }

    public async Task<AuthResult> RefreshTokenAsync(
        string accessToken,
        string refreshToken,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to refresh token");

        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        if (principal.Identity?.Name is null)
        {
            _logger.LogWarning("Invalid access token or refresh token");
            return new AuthResult { Succeeded = false, Message = "Invalid access token or refresh token" };
        }

        var user = await _userManager.FindByNameAsync(principal.Identity.Name);
        if (!IsRefreshTokenValid(user, refreshToken))
        {
            _logger.LogWarning("Invalid refresh token for user: {UserName}", principal.Identity.Name);
            return new AuthResult { Succeeded = false, Message = "Invalid access token or refresh token" };
        }

        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        var tokenExpiration = DateTime.UtcNow.AddMinutes(_tokenService.GetAccessTokenExpirationMinutes());

        user!.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(RefreshTokenExpiryDays);
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Token refreshed successfully for user: {UserName}", principal.Identity.Name);

        return new AuthResult
        {
            Succeeded = true,
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            TokenExpiration = tokenExpiration,
            Message = "Token refreshed successfully"
        };
    }

    public async Task RevokeTokenAsync(
        string username,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Attempting to revoke token for user: {UserName}", username);

        var user = await _userManager.FindByNameAsync(username);
        if (user == null)
        {
            _logger.LogWarning("Failed to revoke token. User not found: {UserName}", username);
            throw new ArgumentException("Invalid username");
        }

        user.RefreshToken = null;
        await _userManager.UpdateAsync(user);

        _logger.LogInformation("Token revoked successfully for user: {UserName}", username);
    }

    public async Task<AuthResult> ForgotPasswordAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Password reset requested for email: {Email}", email);

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            _logger.LogWarning("Password reset requested for non-existent email: {Email}", email);
            return new AuthResult
            {
                Succeeded = true,
                Message = "If that email address is in our system, we have sent a password reset link to it."
            };
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = GeneratePasswordResetUrl(email, encodedToken);

        await _emailService.SendEmailAsync(
            email,
            "Reset Password",
            GeneratePasswordResetMessage(callbackUrl));

        _logger.LogInformation("Password reset email sent to: {Email}", email);

        return new AuthResult
        {
            Succeeded = true,
            Message = "If that email address is in our system, we have sent a password reset link to it."
        };
    }

    public async Task<AuthResult> ResetPasswordAsync(
        string email,
        string token,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Password reset attempt for user: {Email}", email);

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            _logger.LogWarning("Password reset failed. User not found: {Email}", email);
            return new AuthResult { Succeeded = false, Message = "Password reset failed." };
        }

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
        var result = await _userManager.ResetPasswordAsync(user, decodedToken, newPassword);

        if (result.Succeeded)
        {
            _logger.LogInformation("Password reset successful for user: {Email}", email);
            return new AuthResult { Succeeded = true, Message = "Password has been reset successfully." };
        }
        else
        {
            _logger.LogWarning("Password reset failed for user: {Email}. Errors: {Errors}",
                email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return new AuthResult
            {
                Succeeded = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
    }

    public async Task<AuthResult> ChangePasswordAsync(
        string username,
        string currentPassword,
        string newPassword,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Password change attempt for user: {UserName}", username);

        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
        {
            _logger.LogWarning("Password change failed. User not found: {UserName}", username);
            return new AuthResult { Succeeded = false, Message = "User not found." };
        }

        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (result.Succeeded)
        {
            _logger.LogInformation("Password changed successfully for user: {UserName}", username);
            return new AuthResult { Succeeded = true, Message = "Password changed successfully." };
        }
        else
        {
            _logger.LogWarning("Password change failed for user: {UserName}. Errors: {Errors}",
                username, string.Join(", ", result.Errors.Select(e => e.Description)));
            return new AuthResult
            {
                Succeeded = false,
                Message = string.Join(", ", result.Errors.Select(e => e.Description))
            };
        }
    }

    public async Task<AuthResult> ConfirmEmailAsync(
        string userId,
        string token,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Email confirmation attempt for user ID: {UserId}", userId);

        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            _logger.LogWarning("Email confirmation failed. User not found: {UserId}", userId);
            return new AuthResult { Succeeded = false, Message = "User not found." };
        }

        var decodedToken = Uri.UnescapeDataString(token);
        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (result.Succeeded)
        {
            _logger.LogInformation("Email confirmed successfully for user: {Email}", user.Email);
            return new AuthResult { Succeeded = true, Message = "Thank you for confirming your email." };
        }
        else
        {
            _logger.LogWarning("Email confirmation failed for user: {Email}. Errors: {Errors}",
                user.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
            return new AuthResult { Succeeded = false, Message = "Email confirmation failed." };
        }
    }

    public async Task<AuthResult> ResendEmailConfirmationAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Resend email confirmation requested for: {Email}", email);

        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            _logger.LogWarning("Resend email confirmation requested for non-existent email: {Email}", email);
            return new AuthResult
            {
                Succeeded = true,
                Message = "If that email address is in our system, we have sent a confirmation link to it."
            };
        }

        if (await _userManager.IsEmailConfirmedAsync(user))
        {
            _logger.LogInformation("Resend email confirmation requested for already confirmed email: {Email}", email);
            return new AuthResult { Succeeded = false, Message = "This email is already confirmed." };
        }

        var (_, callbackUrl) = await GenerateEmailConfirmationTokenAndUrl(user);

        await _emailService.SendEmailAsync(
            email,
            "Confirm your email",
            GenerateEmailConfirmationMessage(callbackUrl));

        _logger.LogInformation("Confirmation email resent to: {Email}", email);

        return new AuthResult { Succeeded = true, Message = "Verification email sent. Please check your email." };
    }

    #region Private Helper Methods

    private Task<List<Claim>> GenerateAuthClaims(ApplicationUser user, string? userRole)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role, userRole ?? string.Empty)
        };

        return Task.FromResult(authClaims);
    }

    private async Task<(string encodedToken, string callbackUrl)> GenerateEmailConfirmationTokenAndUrl(
        ApplicationUser user)
    {
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token);
        var callbackUrl = $"{_secretOptions.AppUrl}/api/auth/confirm-email?userId={user.Id}&token={encodedToken}";

        return (encodedToken, callbackUrl);
    }

    private static string GenerateEmailConfirmationMessage(string callbackUrl) =>
        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

    private string GeneratePasswordResetUrl(string email, string encodedToken) =>
        $"{_secretOptions.AppUrl}/api/Auth/reset-password?email={email}&token={encodedToken}";

    private static string GeneratePasswordResetMessage(string callbackUrl) =>
        $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";

    private static bool IsRefreshTokenValid(ApplicationUser? user, string refreshToken) =>
        user?.RefreshToken == refreshToken && user.RefreshTokenExpiryTime > DateTime.UtcNow;

    #endregion
}