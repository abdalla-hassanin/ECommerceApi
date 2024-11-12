using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record ChangePasswordCommand(string Username, string CurrentPassword, string NewPassword)
    : IRequest<ApiResponse<bool>>;

public class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.CurrentPassword).NotEmpty().WithMessage("Current password is required.");
        RuleFor(x => x.NewPassword).NotEmpty().MinimumLength(8)
            .WithMessage("New password must be at least 8 characters long.");
    }
}

public class ChangePasswordCommandHandler(
    IAuthService authService,
    ILogger<ChangePasswordCommandHandler> logger) 
    : IRequestHandler<ChangePasswordCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to change password for user {Username}", request.Username);

        try
        {
            var result = await authService.ChangePasswordAsync(request.Username, request.CurrentPassword,
                request.NewPassword, cancellationToken);

            if (result.Succeeded)
            {
                logger.LogInformation("Password changed successfully for user {Username}", request.Username);
                return ApiResponse<bool>.Factory.Success(result.Succeeded, result.Message);
            }
            else
            {
                logger.LogWarning("Failed to change password for user {Username}. Reason: {Message}", request.Username, result.Message);
                return ApiResponse<bool>.Factory.BadRequest(result.Message ?? "An error occurred while changing password.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while changing password for user {Username}", request.Username);
            throw;
        }
    }
}