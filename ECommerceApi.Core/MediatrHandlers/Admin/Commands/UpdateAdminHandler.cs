using AutoMapper;
using ECommerceApi.Core.Base.Response;
using ECommerceApi.Service.IService;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Core.MediatrHandlers.Admin.Commands;

public record UpdateAdminCommand(
    string AdminId,
    string Position
) : IRequest<ApiResponse<AdminDto>>;

public class UpdateAdminCommandValidator : AbstractValidator<UpdateAdminCommand>
{
    public UpdateAdminCommandValidator()
    {
        RuleFor(x => x.AdminId).NotEmpty().WithMessage("Admin ID is required.");
        RuleFor(x => x.Position).NotEmpty().MaximumLength(100).WithMessage("Position is required and must not exceed 100 characters.");
    }
}

public class UpdateAdminHandler(
    IAdminService adminService,
    IMapper mapper,
    ILogger<UpdateAdminHandler> logger) : IRequestHandler<UpdateAdminCommand, ApiResponse<AdminDto>>
{
    public async Task<ApiResponse<AdminDto>> Handle(UpdateAdminCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating admin with ID {AdminId}", request.AdminId);
        try
        {
            var existingAdmin = await adminService.GetAdminByIdAsync(request.AdminId, cancellationToken);
            if (existingAdmin is null)
            {
                logger.LogWarning("Admin with ID {AdminId} not found", request.AdminId);
                return ApiResponse<AdminDto>.Factory.NotFound("Admin not found");
            }

            mapper.Map(request, existingAdmin);
            var updatedAdmin = await adminService.UpdateAdminAsync(existingAdmin, cancellationToken);
            logger.LogInformation("Admin with ID {AdminId} updated successfully", request.AdminId);
            return ApiResponse<AdminDto>.Factory.Success(mapper.Map<AdminDto>(updatedAdmin));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while updating admin with ID {AdminId}", request.AdminId);
            throw;
        }
    }
}
