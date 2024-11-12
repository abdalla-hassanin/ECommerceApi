using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Customer.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Customer;

public class CustomerMappingProfile : Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Data.Entities.Customer, CustomerDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.ApplicationUser.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.ApplicationUser.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.ApplicationUser.Email));
        CreateMap<UpdateCustomerCommand, Data.Entities.Customer>();
    }
}
