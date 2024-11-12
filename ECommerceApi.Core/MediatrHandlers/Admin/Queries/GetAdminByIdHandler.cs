using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Admin.Queries;

public record GetAdminByIdQuery(string AdminId) : IRequest<ApiResponse<AdminDto>>;

public class GetAdminByIdHandler(
    IAdminService adminService,
    IMapper mapper,
    ILogger<GetAdminByIdHandler> logger) : IRequestHandler<GetAdminByIdQuery, ApiResponse<AdminDto>>
{
    public async Task<ApiResponse<AdminDto>> Handle(GetAdminByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting admin with ID {AdminId}", request.AdminId);
        try
        {
            var admin = await adminService.GetAdminByIdAsync(request.AdminId, cancellationToken);
            if (admin is null)
            {
                logger.LogWarning("Admin with ID {AdminId} not found", request.AdminId);
                return ApiResponse<AdminDto>.Factory.NotFound("Admin not found");
            }
            logger.LogInformation("Admin with ID {AdminId} retrieved successfully", request.AdminId);
            return ApiResponse<AdminDto>.Factory.Success(mapper.Map<AdminDto>(admin));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting admin with ID {AdminId}", request.AdminId);
            throw;
        }
    }
}
