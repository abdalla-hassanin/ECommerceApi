using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Admin.Commands;

public record DeleteAdminCommand(string AdminId) : IRequest<ApiResponse<bool>>;

public class DeleteAdminHandler(
    IAdminService adminService,
    ILogger<DeleteAdminHandler> logger) : IRequestHandler<DeleteAdminCommand, ApiResponse<bool>>
{
    public async Task<ApiResponse<bool>> Handle(DeleteAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting admin with ID {AdminId}", request.AdminId);
        try
        {
            await adminService.DeleteAdminAsync(request.AdminId, cancellationToken);
            logger.LogInformation("Admin with ID {AdminId} deleted successfully", request.AdminId);
            return ApiResponse<bool>.Factory.Success(true, "Admin deleted successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while deleting admin with ID {AdminId}", request.AdminId);
            throw;
        }
    }
}
