using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.ProductImage.Commands;

namespace ECommerceApi.Core.MediatrHandlers.ProductImage
{
    public class ProductImageMappingProfile : Profile
    {
        public ProductImageMappingProfile()
        {
            CreateMap<Data.Entities.ProductImage, ProductImageDto>();
            CreateMap<CreateProductImageCommand, Data.Entities.ProductImage>()
                .ForMember(dest => dest.ImageId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
            CreateMap<UpdateProductImageCommand, Data.Entities.ProductImage>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}