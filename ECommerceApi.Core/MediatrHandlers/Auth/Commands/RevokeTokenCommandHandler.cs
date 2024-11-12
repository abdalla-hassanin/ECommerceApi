using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record RevokeTokenCommand(string Username) : IRequest<ApiResponse<bool>>;

public class RevokeTokenCommandValidator : AbstractValidator<RevokeTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
    }
}

public class RevokeTokenCommandHandler(
    IAuthService authService,
    ILogger<RevokeTokenCommandHandler> logger) 
    : IRequestHandler<RevokeTokenCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to revoke token for user {Username}", request.Username);

        try
        {
            await authService.RevokeTokenAsync(request.Username, cancellationToken);
            logger.LogInformation("Token revoked successfully for user {Username}", request.Username);
            return ApiResponse<bool>.Factory.Success(true, "Token revoked successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while revoking token for user {Username}", request.Username);
            throw;
        }
    }
}