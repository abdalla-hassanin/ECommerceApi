using ECommerceApi.Core.Base.Response;
using ECommerceApi.Core.MediatrHandlers.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace ECommerceApi.Api.ResponseExample;

public class RegisterCustomerResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Created(true, "Customer registered successfully. Please check your email to confirm your account.");
    }
}

public class RegisterAdminResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Created(true, "Admin registered successfully. Please check your email to confirm your account.");
    }
}

public class LoginResponseExample : IExamplesProvider<ApiResponse<AuthDto>>
{
    public ApiResponse<AuthDto> GetExamples()
    {
        var authDto = new AuthDto(
            Role:"Admin",
            Id: "01HF3WFKX1KPY89WNJRXJ6V18N",
            Token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            RefreshToken: "8ef0g3f0-c6d7-6fc0-1h2e-g9d0345678",
            TokenExpiration: DateTime.UtcNow.AddHours(1)
        );

        return ApiResponse<AuthDto>.Factory.Success(authDto, "Login successful");
    }
}

public class RefreshTokenResponseExample : IExamplesProvider<ApiResponse<AuthDto>>
{
    public ApiResponse<AuthDto> GetExamples()
    {
        var authDto = new AuthDto(
            Role:"",
            Id: null,
            Token: "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
            RefreshToken: "9fg1h4g1-d7e8-7gd1-2i3f-h0e1456789",
            TokenExpiration: DateTime.UtcNow.AddHours(1)
        );

        return ApiResponse<AuthDto>.Factory.Success(authDto, "Token refreshed successfully");
    }
}

public class RevokeTokenResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Token revoked successfully");
    }
}

public class ForgotPasswordResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Password reset email sent successfully");
    }
}

public class ResetPasswordResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Password reset successfully");
    }
}

public class ConfirmEmailResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Email confirmed successfully");
    }
}

public class ResendEmailConfirmationResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Email confirmation resent successfully");
    }
}

public class ChangePasswordResponseExample : IExamplesProvider<ApiResponse<bool>>
{
    public ApiResponse<bool> GetExamples()
    {
        return ApiResponse<bool>.Factory.Success(true, "Password changed successfully");
    }
}

public class BadRequestAuthResponseExample : IExamplesProvider<ApiResponse<AuthDto>>
{
    public ApiResponse<AuthDto> GetExamples()
    {
        return ApiResponse<AuthDto>.Factory.BadRequest(
            "Invalid input data",
            new List<string> { "Email is required", "Password must be at least 8 characters long" }
        );
    }
}

public class UnauthorizedAuthResponseExample : IExamplesProvider<ApiResponse<AuthDto>>
{
    public ApiResponse<AuthDto> GetExamples()
    {
        return ApiResponse<AuthDto>.Factory.Unauthorized("Invalid credentials");
    }
}