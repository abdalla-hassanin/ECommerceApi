using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Category.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Category;

public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Data.Entities.Category, CategoryDto>();
        CreateMap<CreateCategoryCommand, Data.Entities.Category>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateCategoryCommand, Data.Entities.Category>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}