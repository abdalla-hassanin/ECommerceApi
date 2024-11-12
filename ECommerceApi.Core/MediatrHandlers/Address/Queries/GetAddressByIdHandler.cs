using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Address.Queries;

public record GetAddressByIdQuery(string AddressId) : IRequest<ApiResponse<AddressDto>>;

public class GetAddressByIdHandler(
    IAddressService addressService,
    IMapper mapper,
    ILogger<GetAddressByIdHandler> logger) : IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressDto>>
{
    public async Task<ApiResponse<AddressDto>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting address with ID {AddressId}", request.AddressId);

        try
        {
            var address = await addressService.GetAddressByIdAsync(request.AddressId, cancellationToken);
            if (address is null)
            {
                logger.LogWarning("Address with ID {AddressId} not found", request.AddressId);
                return ApiResponse<AddressDto>.Factory.NotFound("Address not found");
            }

            var addressDto = mapper.Map<AddressDto>(address);
            logger.LogDebug("Mapped Address entity to AddressDto");

            return ApiResponse<AddressDto>.Factory.Success(addressDto);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting address with ID {AddressId}", request.AddressId);
            throw;
        }
    }
}
