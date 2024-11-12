using System.Linq.Expressions;
using ECommerceApi.Data.Entities;
using ECommerceApi.Infrastructure.Base.Specifications;

namespace ECommerceApi.Service.Specification;

public class AddressSpecifications(Expression<Func<Address, bool>> criteria)
    : BaseSpecification<Address>(criteria);