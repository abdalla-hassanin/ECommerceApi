using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.ProductVariant.Commands;

namespace ECommerceApi.Core.MediatrHandlers.ProductVariant
{
    public class ProductVariantMappingProfile : Profile
    {
        public ProductVariantMappingProfile()
        {
            CreateMap<Data.Entities.ProductVariant, ProductVariantDto>();
            CreateMap<CreateProductVariantCommand, Data.Entities.ProductVariant>()
                .ForMember(dest => dest.VariantId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<UpdateProductVariantCommand, Data.Entities.ProductVariant>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}