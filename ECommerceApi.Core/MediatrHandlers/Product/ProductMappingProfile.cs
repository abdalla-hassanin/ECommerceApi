using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Product.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Product;

public class ProductMappingProfile : Profile
{
    public ProductMappingProfile()
    {
        CreateMap<Data.Entities.Product, ProductDto>();
        CreateMap<CreateProductCommand, Data.Entities.Product>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateProductCommand, Data.Entities.Product>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}