using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Address.Commands;

public record DeleteAddressCommand(string AddressId) : IRequest<ApiResponse<bool>>;

public class DeleteAddressHandler(
    IAddressService addressService,
    ILogger<DeleteAddressHandler> logger) : IRequestHandler<DeleteAddressCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting address with ID {AddressId}", request.AddressId);

        try
        {
            await addressService.DeleteAddressAsync(request.AddressId, cancellationToken);
            logger.LogInformation("Address with ID {AddressId} deleted successfully", request.AddressId);
            return ApiResponse<bool>.Factory.Success(true, "Address deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting address with ID {AddressId}", request.AddressId);
            throw;
        }
    }
}
