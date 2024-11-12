using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public static class AdminSpecifications
{
    public sealed class ByAdminId : BaseSpecification<Admin>
    {
        public ByAdminId(string adminId) : base(a => a.AdminId == adminId)
        {
            AddInclude(a => a.ApplicationUser);
        }
    }
    public sealed class ByUserId : BaseSpecification<Admin>
    {
        public ByUserId(string userId) : base(a => a.ApplicationUser.Id == userId)
        {
            AddInclude(a => a.ApplicationUser);
        }
    }
    public sealed class AllAdmins : BaseSpecification<Admin>
    {
        public AllAdmins() : base(a => true)
        {
            AddInclude(a => a.ApplicationUser);
            ApplyOrderBy(a => a.AdminId);
        }
    }
}