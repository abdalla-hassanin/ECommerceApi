using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Address.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Address;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<Data.Entities.Address, AddressDto>();
        CreateMap<CreateAddressCommand, Data.Entities.Address>()
            .ForMember(dest => dest.AddressId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateAddressCommand, Data.Entities.Address>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}