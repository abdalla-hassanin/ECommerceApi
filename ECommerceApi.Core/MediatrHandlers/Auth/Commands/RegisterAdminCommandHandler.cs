using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Data.Entities;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record RegisterAdminCommand(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string Position
) : IRequest<ApiResponse<bool>>;

public class RegisterAdminCommandValidator : AbstractValidator<RegisterAdminCommand>
{
    public RegisterAdminCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.Position).NotEmpty().WithMessage("Position is required.");
    }
}

public class RegisterAdminCommandHandler(
    IAuthService authService,
    IAdminService adminService,
    UserManager<ApplicationUser> userManager,
    ILogger<RegisterAdminCommandHandler> logger)
    : IRequestHandler<RegisterAdminCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(RegisterAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to register new admin with username {Username}", request.Username);

        try
        {
            var existingEmail = await userManager.FindByEmailAsync(request.Email);
            if (existingEmail != null)
            {
                logger.LogWarning("Email {Email} is already registered", request.Email);
                return ApiResponse<bool>.Factory.BadRequest("This email is already registered");
            }

            var existingUsername = await userManager.FindByNameAsync(request.Username);
            if (existingUsername != null)
            {
                logger.LogWarning("Username {Username} is already taken", request.Username);
                return ApiResponse<bool>.Factory.BadRequest("This username is already taken");
            }

            
            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            var authResult = await authService.RegisterUserAsync(user, request.Password, "Admin", cancellationToken);

            if (!authResult.Succeeded)
            {
                logger.LogWarning("Admin registration failed for username {Username}. Reason: {Message}", request.Username, authResult.Message);
                return ApiResponse<bool>.Factory.BadRequest(authResult.Message ?? "Registration failed");
            }

            var createdUser = await userManager.FindByNameAsync(request.Username);
            if (createdUser == null)
            {
                logger.LogWarning("User creation failed for username {Username}", request.Username);
                return ApiResponse<bool>.Factory.BadRequest("User creation failed");
            }

            var admin = new Data.Entities.Admin
            {
                ApplicationUserId = createdUser.Id,
                Position = request.Position
            };

            await adminService.CreateAdminAsync(admin, cancellationToken);

            logger.LogInformation("Admin registered successfully with username {Username}", request.Username);
            return ApiResponse<bool>.Factory.Created(true, authResult.Message ?? "Admin registered successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during admin registration for username {Username}", request.Username);
            throw;
        }
    }
}