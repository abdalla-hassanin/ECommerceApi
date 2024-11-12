using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record RefreshTokenCommand(string AccessToken, string RefreshToken) : IRequest<ApiResponse<AuthDto>>;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty().WithMessage("Access token is required.");
        RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
    }
}

public class RefreshTokenCommandHandler(
    IAuthService authService,
    IMapper mapper,
    ILogger<RefreshTokenCommandHandler> logger) 
    : IRequestHandler<RefreshTokenCommand, ApiResponse<AuthDto>>
{
    public async Task<ApiResponse<AuthDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to refresh token");

        try
        {
            var result = await authService.RefreshTokenAsync(request.AccessToken, request.RefreshToken, cancellationToken);

            if (result.Succeeded)
            {
                var authDto = mapper.Map<AuthDto>(result);
                logger.LogInformation("Token refreshed successfully for user {UserId}", result.UserId);
                return ApiResponse<AuthDto>.Factory.Success(authDto, result.Message);
            }

            logger.LogWarning("Token refresh failed. Reason: {Message}", result.Message);
            return ApiResponse<AuthDto>.Factory.BadRequest(result.Message ?? "Token refresh failed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during token refresh");
            throw;
        }
    }
}