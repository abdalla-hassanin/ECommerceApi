using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Customer.Commands;

public record UpdateCustomerCommand(
    string CustomerId,
    DateTime? DateOfBirth,
    string? Gender,
    string? Bio,
    IFormFile? ImageFile,
    string? Language
) : IRequest<ApiResponse<CustomerDto>>;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().Length(26).WithMessage("Valid ULID is required.");
        RuleFor(x => x.DateOfBirth).LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
        RuleFor(x => x.Gender).MaximumLength(50).WithMessage("Gender must not exceed 50 characters.");
        RuleFor(x => x.Bio).MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");
        RuleFor(x => x.Language).MaximumLength(50).WithMessage("Language must not exceed 50 characters.");
    }
}

public class UpdateCustomerHandler(
    ICustomerService customerService,
    IMapper mapper,
    ILogger<UpdateCustomerHandler> logger) : IRequestHandler<UpdateCustomerCommand, ApiResponse<CustomerDto>>
{
    public async Task<ApiResponse<CustomerDto>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating customer with ID {CustomerId}", request.CustomerId);
        try
        {
            var existingCustomer = await customerService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
            if (existingCustomer is null)
            {
                logger.LogWarning("Customer with ID {CustomerId} not found", request.CustomerId);
                return ApiResponse<CustomerDto>.Factory.NotFound("Customer not found");
            }

            mapper.Map(request, existingCustomer);
            var updatedCustomer = await customerService.UpdateCustomerAsync(existingCustomer,request.ImageFile, cancellationToken);
            logger.LogInformation("Customer with ID {CustomerId} updated successfully", request.CustomerId);
            return ApiResponse<CustomerDto>.Factory.Success(mapper.Map<CustomerDto>(updatedCustomer));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating customer with ID {CustomerId}", request.CustomerId);
            throw;
        }
    }
}
