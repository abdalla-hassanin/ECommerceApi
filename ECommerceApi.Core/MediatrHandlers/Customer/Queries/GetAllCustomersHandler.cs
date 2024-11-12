using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Customer.Queries;

public record GetAllCustomersQuery : IRequest<ApiResponse<IReadOnlyList<CustomerDto>>>;

public class GetAllCustomersHandler(
    ICustomerService customerService,
    IMapper mapper,
    ILogger<GetAllCustomersHandler> logger) : IRequestHandler<GetAllCustomersQuery, ApiResponse<IReadOnlyList<CustomerDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<CustomerDto>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all customers");
        try
        {
            var customers = await customerService.GetAllCustomersAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} customers", customers.Count);
            return ApiResponse<IReadOnlyList<CustomerDto>>.Factory.Success(mapper.Map<IReadOnlyList<CustomerDto>>(customers));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all customers");
            throw;
        }
    }
}
