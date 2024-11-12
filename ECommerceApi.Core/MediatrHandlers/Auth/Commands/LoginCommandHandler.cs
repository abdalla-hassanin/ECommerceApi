using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record LoginCommand(string Email, string Password) : IRequest<ApiResponse<AuthDto>>;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
    }
}

public class LoginCommandHandler(
    IAuthService authService,
    IAdminService adminService,
    ICustomerService customerService,
    IMapper mapper,
    ILogger<LoginCommandHandler> logger) 
    : IRequestHandler<LoginCommand, ApiResponse<AuthDto>>
{
    public async Task<ApiResponse<AuthDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting login for user with email {Email}", request.Email);

        try
        {
            var result = await authService.LoginAsync(request.Email, request.Password, cancellationToken);

            if (result.Succeeded)
            {
                var authDto = mapper.Map<AuthDto>(result);

                if (result.Role == "Admin")
                {
                    var admin = await adminService.GetAdminByUserIdAsync(result.UserId!, cancellationToken);
                    authDto = authDto with { Id = admin?.AdminId };
                    logger.LogInformation("Admin login successful for user {UserId}", result.UserId);
                }
                else if (result.Role == "Customer")
                {
                    var customer = await customerService.GetCustomerByUserIdAsync(result.UserId!, cancellationToken);
                    authDto = authDto with { Id = customer?.CustomerId };
                    logger.LogInformation("Customer login successful for user {UserId}", result.UserId);
                }

                return ApiResponse<AuthDto>.Factory.Success(authDto, result.Message);
            }

            logger.LogWarning("Login failed for user with email {Email}. Reason: {Message}", request.Email, result.Message);
            return ApiResponse<AuthDto>.Factory.Unauthorized(result.Message ?? "Login failed");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during login attempt for user with email {Email}", request.Email);
            throw;
        }
    }
}