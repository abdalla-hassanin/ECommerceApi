using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Wishlist.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Wishlist
{
    public class WishlistMappingProfile : Profile
    {
        public WishlistMappingProfile()
        {
            CreateMap<Data.Entities.Wishlist, WishlistDto>();
            CreateMap<AddToWishlistCommand, Data.Entities.Wishlist>()
                .ForMember(dest => dest.WishlistId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        }
    }
}