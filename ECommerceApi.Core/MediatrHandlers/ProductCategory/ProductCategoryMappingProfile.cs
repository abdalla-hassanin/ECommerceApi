using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.ProductCategory.Commands;

namespace ECommerceApi.Core.MediatrHandlers.ProductCategory;

public class ProductCategoryMappingProfile : Profile
{
    public ProductCategoryMappingProfile()
    {
        CreateMap<Data.Entities.ProductCategory, ProductCategoryDto>();
        CreateMap<AddProductToCategoryCommand, Data.Entities.ProductCategory>();
    }
}