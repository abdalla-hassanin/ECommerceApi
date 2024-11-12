using ECommerceApi.Core.Base.Response;
using ECommerceApi.Data.Entities;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Auth.Commands;

public record RegisterCustomerCommand(
    string Username,
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTime? DateOfBirth,
    string? Gender,
    string? Bio,
    IFormFile ImageFile,
    string? Language
) : IRequest<ApiResponse<bool>>;

public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
{
    public RegisterCustomerCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required.");
        RuleFor(x => x.Password).NotEmpty().MinimumLength(8)
            .WithMessage("Password must be at least 8 characters long.");
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required.");
        RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required.");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.Gender).MaximumLength(50).WithMessage("Gender must not exceed 50 characters.");
        RuleFor(x => x.Bio).MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");
        RuleFor(x => x.Language).MaximumLength(50).WithMessage("Language must not exceed 50 characters.");
        RuleFor(x=>x.ImageFile).NotNull().WithMessage("Profile picture is required.");
    }
}

public class RegisterCustomerCommandHandler(
    IAuthService authService,
    ICustomerService customerService,
    UserManager<ApplicationUser> userManager,
    ILogger<RegisterCustomerCommandHandler> logger) 
    : IRequestHandler<RegisterCustomerCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(RegisterCustomerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to register new customer with username {Username}", request.Username);

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

            var authResult = await authService.RegisterUserAsync(user, request.Password, "Customer", cancellationToken);

            if (!authResult.Succeeded)
            {
                logger.LogWarning("Customer registration failed for username {Username}. Reason: {Message}", request.Username, authResult.Message);
                return ApiResponse<bool>.Factory.BadRequest(authResult.Message ?? "Registration failed");
            }

            var createdUser = await userManager.FindByNameAsync(request.Username);
            if (createdUser == null)
            {
                logger.LogWarning("User creation failed for username {Username}", request.Username);
                return ApiResponse<bool>.Factory.BadRequest("User creation failed");
            }
            
            var customer = new Data.Entities.Customer
            {
                ApplicationUserId = createdUser.Id,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                Bio = request.Bio,
                Language = request.Language,
                ProfilePictureUrl =""
                
            };

            await customerService.CreateCustomerAsync(customer,request.ImageFile, cancellationToken);

            logger.LogInformation("Customer registered successfully with username {Username}", request.Username);
            return ApiResponse<bool>.Factory.Created(true, authResult.Message ?? "Customer registered successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during customer registration for username {Username}", request.Username);
            throw;
        }
    }
}