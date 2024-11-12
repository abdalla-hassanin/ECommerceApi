using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Customer.Commands;

public record DeleteCustomerCommand(string CustomerId) : IRequest<ApiResponse<bool>>;

public class DeleteCustomerHandler(
    ICustomerService customerService,
    ILogger<DeleteCustomerHandler> logger) : IRequestHandler<DeleteCustomerCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting customer with ID {CustomerId}", request.CustomerId);
        try
        {
            await customerService.DeleteCustomerAsync(request.CustomerId, cancellationToken);
            logger.LogInformation("Customer with ID {CustomerId} deleted successfully", request.CustomerId);
            return ApiResponse<bool>.Factory.Success(true, "Customer deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting customer with ID {CustomerId}", request.CustomerId);
            throw;
        }
    }
}
