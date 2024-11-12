using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record ResetPasswordCommand(string Email, string Token, string NewPassword) : IRequest<ApiResponse<bool>>;

public class ResetPasswordCommandValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8).WithMessage("New password must be at least 8 characters long.");
    }
}

public class ResetPasswordCommandHandler(
    IAuthService authService,
    ILogger<ResetPasswordCommandHandler> logger) 
    : IRequestHandler<ResetPasswordCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to reset password for email {Email}", request.Email);

        try
        {
            var result = await authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword, cancellationToken);
            
            if (result.Succeeded)
            {
                logger.LogInformation("Password reset successfully for email {Email}", request.Email);
                return ApiResponse<bool>.Factory.Success(result.Succeeded, result.Message);
            }
            else
            {
                logger.LogWarning("Failed to reset password for email {Email}. Reason: {Message}", request.Email, result.Message);
                return ApiResponse<bool>.Factory.BadRequest(result.Message ?? "Failed to reset password");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while resetting password for email {Email}", request.Email);
            throw;
        }
    }
}