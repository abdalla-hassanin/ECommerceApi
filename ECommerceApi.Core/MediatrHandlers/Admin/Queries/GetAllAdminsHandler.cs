using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Admin.Queries;

public record GetAllAdminsQuery : IRequest<ApiResponse<IReadOnlyList<AdminDto>>>;

public class GetAllAdminsHandler(
    IAdminService adminService,
    IMapper mapper,
    ILogger<GetAllAdminsHandler> logger) : IRequestHandler<GetAllAdminsQuery, ApiResponse<IReadOnlyList<AdminDto>>>
{
    public async Task<ApiResponse<IReadOnlyList<AdminDto>>> Handle(GetAllAdminsQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting all admins");
        try
        {
            var admins = await adminService.GetAllAdminsAsync(cancellationToken);
            logger.LogInformation("Retrieved {Count} admins", admins.Count);
            return ApiResponse<IReadOnlyList<AdminDto>>.Factory.Success(mapper.Map<IReadOnlyList<AdminDto>>(admins));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting all admins");
            throw;
        }
    }
}
