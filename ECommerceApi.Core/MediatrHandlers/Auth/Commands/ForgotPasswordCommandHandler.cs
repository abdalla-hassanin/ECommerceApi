using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record ForgotPasswordCommand(string Email) : IRequest<ApiResponse<bool>>;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
    }
}

public class ForgotPasswordCommandHandler(
    IAuthService authService,
    ILogger<ForgotPasswordCommandHandler> logger) 
    : IRequestHandler<ForgotPasswordCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to initiate forgot password process for email {Email}", request.Email);

        try
        {
            var result = await authService.ForgotPasswordAsync(request.Email, cancellationToken);

            if (result.Succeeded)
            {
                logger.LogInformation("Forgot password email sent successfully for email {Email}", request.Email);
                return ApiResponse<bool>.Factory.Success(result.Succeeded, result.Message);
            }
            else
            {
                logger.LogWarning("Failed to send forgot password email for {Email}. Reason: {Message}", request.Email, result.Message);
                return ApiResponse<bool>.Factory.BadRequest(result.Message ?? "Failed to send password reset email");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing forgot password request for email {Email}", request.Email);
            throw;
        }
    }
}