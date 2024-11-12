using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base;
using ECommerceApi.Service.IService;
using ECommerceApi.Service.Specification;
using Microsoft.Extensions.Logging;

namespace ECommerceApi.Service.Service;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AdminService> _logger;

    public AdminService(IUnitOfWork unitOfWork, ILogger<AdminService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Admin?> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting admin with ID: {AdminId}", adminId);
        var spec = new AdminSpecifications.ByAdminId(adminId);
        var admins = await _unitOfWork.Repository<Admin>().ListAsync(spec, cancellationToken);
        return admins.FirstOrDefault();
    }

    public async Task<Admin?> GetAdminByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting admin by user ID: {UserId}", userId);
        var spec = new AdminSpecifications.ByUserId(userId);
        var admins = await _unitOfWork.Repository<Admin>().ListAsync(spec, cancellationToken);
        return admins.FirstOrDefault();
    }

    public async Task<IReadOnlyList<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all admins");
        var spec = new AdminSpecifications.AllAdmins();
        return await _unitOfWork.Repository<Admin>().ListAsync(spec, cancellationToken);
    }

    public async Task<Admin> CreateAdminAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Creating new admin");
        await _unitOfWork.Repository<Admin>().AddAsync(admin, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Admin created with ID: {AdminId}", admin.AdminId);
        return admin;
    }

    public async Task<Admin> UpdateAdminAsync(Admin admin, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Updating admin with ID: {AdminId}", admin.AdminId);
        await _unitOfWork.Repository<Admin>().UpdateAsync(admin, cancellationToken);
        await _unitOfWork.CompleteAsync(cancellationToken);
        _logger.LogInformation("Admin updated successfully");
        return admin;
    }

    public async Task DeleteAdminAsync(string adminId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting admin with ID: {AdminId}", adminId);
        var admin = await GetAdminByIdAsync(adminId, cancellationToken);
        if (admin is not null)
        {
            await _unitOfWork.Repository<Admin>().DeleteAsync(admin, cancellationToken);
            await _unitOfWork.CompleteAsync(cancellationToken);
            _logger.LogInformation("Admin deleted successfully");
        }
        else
        {
            _logger.LogWarning("Attempted to delete non-existent admin with ID: {AdminId}", adminId);
        }
    }
}