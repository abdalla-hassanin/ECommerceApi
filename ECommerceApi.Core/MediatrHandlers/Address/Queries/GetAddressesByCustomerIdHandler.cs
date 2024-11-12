using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Address.Queries;

public record GetAddressesByCustomerIdQuery(string CustomerId) : IRequest<ApiResponse<IReadOnlyList<AddressDto>>>;

public class GetAddressesByCustomerIdHandler(
    IAddressService addressService,
    IMapper mapper,
    ILogger<GetAddressesByCustomerIdHandler> logger) : IRequestHandler<GetAddressesByCustomerIdQuery, ApiResponse<IReadOnlyList<AddressDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<AddressDto>>> Handle(GetAddressesByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting addresses for customer with ID {CustomerId}", request.CustomerId);

        try
        {
            var addresses = await addressService.GetAddressesByCustomerIdAsync(request.CustomerId, cancellationToken);
            logger.LogDebug("Retrieved {Count} addresses for customer {CustomerId}", addresses.Count, request.CustomerId);

            var addressDtos = mapper.Map<IReadOnlyList<AddressDto>>(addresses);
            logger.LogDebug("Mapped Address entities to AddressDtos");

            return ApiResponse<IReadOnlyList<AddressDto>>.Factory.Success(addressDtos);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting addresses for customer with ID {CustomerId}", request.CustomerId);
            throw;
        }
    }
}
