using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record ConfirmEmailCommand(string UserId, string Token) : IRequest<ApiResponse<bool>>;

public class ConfirmEmailCommandValidator : AbstractValidator<ConfirmEmailCommand>
{
    public ConfirmEmailCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("User ID is required.");
        RuleFor(x => x.Token).NotEmpty().WithMessage("Token is required.");
    }
}

public class ConfirmEmailCommandHandler(
    IAuthService authService,
    ILogger<ConfirmEmailCommandHandler> logger) 
    : IRequestHandler<ConfirmEmailCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to confirm email for user {UserId}", request.UserId);

        try
        {
            var result = await authService.ConfirmEmailAsync(request.UserId, request.Token, cancellationToken);

            if (result.Succeeded)
            {
                logger.LogInformation("Email confirmed successfully for user {UserId}", request.UserId);
                return ApiResponse<bool>.Factory.Success(result.Succeeded, result.Message);
            }
            else
            {
                logger.LogWarning("Failed to confirm email for user {UserId}. Reason: {Message}", request.UserId, result.Message);
                return ApiResponse<bool>.Factory.BadRequest(result.Message ?? "Email confirmation failed");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while confirming email for user {UserId}", request.UserId);
            throw;
        }
    }
}