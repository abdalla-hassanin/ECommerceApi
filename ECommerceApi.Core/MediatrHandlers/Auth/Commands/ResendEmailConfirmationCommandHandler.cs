using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record ResendEmailConfirmationCommand(string Email) : IRequest<ApiResponse<bool>>;

public class ResendEmailConfirmationCommandValidator : AbstractValidator<ResendEmailConfirmationCommand>
{
    public ResendEmailConfirmationCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
    }
}

public class ResendEmailConfirmationCommandHandler(
    IAuthService authService,
    ILogger<ResendEmailConfirmationCommandHandler> logger) 
    : IRequestHandler<ResendEmailConfirmationCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ResendEmailConfirmationCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to resend email confirmation for email {Email}", request.Email);

        try
        {
            var result = await authService.ResendEmailConfirmationAsync(request.Email, cancellationToken);
            
            if (result.Succeeded)
            {
                logger.LogInformation("Email confirmation resent successfully for email {Email}", request.Email);
                return ApiResponse<bool>.Factory.Success(result.Succeeded, result.Message);
            }
            else
            {
                logger.LogWarning("Failed to resend email confirmation for email {Email}. Reason: {Message}", request.Email, result.Message);
                return ApiResponse<bool>.Factory.BadRequest(result.Message ?? "Failed to send email confirmation email");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while resending email confirmation for email {Email}", request.Email);
            throw;
        }
    }
}