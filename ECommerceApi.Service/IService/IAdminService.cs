using ECommerceApi.Data.Entities;

namespace ECommerceApi.Service.IService;

public interface IAdminService
{
    Task<Admin?> GetAdminByIdAsync(string adminId, CancellationToken cancellationToken = default);
    Task<Admin?> GetAdminByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Admin>> GetAllAdminsAsync(CancellationToken cancellationToken = default);
    Task<Admin> CreateAdminAsync(Admin admin, CancellationToken cancellationToken = default);
    Task<Admin> UpdateAdminAsync(Admin admin, CancellationToken cancellationToken = default);
    Task DeleteAdminAsync(string adminId, CancellationToken cancellationToken = default);
}