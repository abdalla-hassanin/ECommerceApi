using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Address.Commands;

public record CreateAddressCommand(
    string CustomerId,
    string AddressLine,
    string City,
    string State,
    string Country,
    string Phone
) : IRequest<ApiResponse<AddressDto>>;

public class CreateAddressCommandValidator : AbstractValidator<CreateAddressCommand>
{
    public CreateAddressCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.")
            .Length(26).WithMessage("CustomerId must be a valid Ulid (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("CustomerId must be a valid Ulid format.");
        RuleFor(x => x.AddressLine).NotEmpty().MaximumLength(100).WithMessage("AddressLine1 is required and must not exceed 100 characters.");
        RuleFor(x => x.City).NotEmpty().MaximumLength(50).WithMessage("City is required and must not exceed 50 characters.");
        RuleFor(x => x.State).NotEmpty().MaximumLength(50).WithMessage("State is required and must not exceed 50 characters.");
        RuleFor(x => x.Country).NotEmpty().MaximumLength(50).WithMessage("Country is required and must not exceed 50 characters.");
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20).WithMessage("Phone is required and must not exceed 20 characters.");
    }
}

public class CreateAddressHandler(
    IAddressService addressService,
    IMapper mapper,
    ILogger<CreateAddressHandler> logger)
    : IRequestHandler<CreateAddressCommand, ApiResponse<AddressDto>>
{
    public async Task<ApiResponse<AddressDto>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating new address for customer {CustomerId}", request.CustomerId);

        try
        {
            var address = mapper.Map<Data.Entities.Address>(request);
            logger.LogDebug("Mapped CreateAddressCommand to Address entity");

            var createdAddress = await addressService.CreateAddressAsync(address, cancellationToken);
            logger.LogInformation("Address created successfully for customer {CustomerId}", request.CustomerId);

            var addressDto = mapper.Map<AddressDto>(createdAddress);
            logger.LogDebug("Mapped created Address entity to AddressDto");

            return ApiResponse<AddressDto>.Factory.Created(addressDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while creating address for customer {CustomerId}", request.CustomerId);
            throw;
        }
    }
}