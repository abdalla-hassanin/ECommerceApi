using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Address.Commands;

public record UpdateAddressCommand(
    string AddressId,
    string AddressLine,
    string City,
    string State,
    string Country,
    string Phone
) : IRequest<ApiResponse<AddressDto>>;

public class UpdateAddressCommandValidator : AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(x => x.AddressId).NotEmpty().WithMessage("AddressId is required.")
            .Length(26).WithMessage("AddressId must be a valid Ulid (26 characters).")
            .Must(id => Ulid.TryParse(id, out _)).WithMessage("AddressId must be a valid Ulid format.");
        RuleFor(x => x.AddressLine).NotEmpty().MaximumLength(100).WithMessage("AddressLine1 is required and must not exceed 100 characters.");
        RuleFor(x => x.City).NotEmpty().MaximumLength(50).WithMessage("City is required and must not exceed 50 characters.");
        RuleFor(x => x.State).NotEmpty().MaximumLength(50).WithMessage("State is required and must not exceed 50 characters.");
        RuleFor(x => x.Country).NotEmpty().MaximumLength(50).WithMessage("Country is required and must not exceed 50 characters.");
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20).WithMessage("Phone is required and must not exceed 20 characters.");
    }
}

public class UpdateAddressHandler(
    IAddressService addressService,
    IMapper mapper,
    ILogger<UpdateAddressHandler> logger) : IRequestHandler<UpdateAddressCommand, ApiResponse<AddressDto>>
{
    public async Task<ApiResponse<AddressDto>> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating address with ID {AddressId}", request.AddressId);

        try
        {
            var existingAddress = await addressService.GetAddressByIdAsync(request.AddressId, cancellationToken);
            if (existingAddress is null)
            {
                logger.LogWarning("Address with ID {AddressId} not found", request.AddressId);
                return ApiResponse<AddressDto>.Factory.NotFound("Address not found");
            }

            mapper.Map(request, existingAddress);
            logger.LogDebug("Mapped UpdateAddressCommand to existing Address entity");

            var updatedAddress = await addressService.UpdateAddressAsync(existingAddress, cancellationToken);
            logger.LogInformation("Address with ID {AddressId} updated successfully", request.AddressId);

            var addressDto = mapper.Map<AddressDto>(updatedAddress);
            logger.LogDebug("Mapped updated Address entity to AddressDto");

            return ApiResponse<AddressDto>.Factory.Success(addressDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating address with ID {AddressId}", request.AddressId);
            throw;
        }
    }
}
