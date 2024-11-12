using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Customer.Queries;

public record GetCustomerByIdQuery(string CustomerId) : IRequest<ApiResponse<CustomerDto>>;

public class GetCustomerByIdHandler(
    ICustomerService customerService,
    IMapper mapper,
    ILogger<GetCustomerByIdHandler> logger) : IRequestHandler<GetCustomerByIdQuery, ApiResponse<CustomerDto>>
{
    public async Task<ApiResponse<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting customer with ID {CustomerId}", request.CustomerId);
        try
        {
            var customer = await customerService.GetCustomerByIdAsync(request.CustomerId, cancellationToken);
            if (customer is null)
            {
                logger.LogWarning("Customer with ID {CustomerId} not found", request.CustomerId);
                return ApiResponse<CustomerDto>.Factory.NotFound("Customer not found");
            }
            logger.LogInformation("Customer with ID {CustomerId} retrieved successfully", request.CustomerId);
            return ApiResponse<CustomerDto>.Factory.Success(mapper.Map<CustomerDto>(customer));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting customer with ID {CustomerId}", request.CustomerId);
            throw;
        }
    }
}
