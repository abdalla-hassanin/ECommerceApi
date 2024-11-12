using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Coupon.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Coupon;

public class CouponMappingProfile : Profile
{
    public CouponMappingProfile()
    {
        CreateMap<Data.Entities.Coupon, CouponDto>();
        CreateMap<CreateCouponCommand, Data.Entities.Coupon>()
            .ForMember(dest => dest.CouponId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateCouponCommand, Data.Entities.Coupon>()
            .ForMember(dest => dest.CouponId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}