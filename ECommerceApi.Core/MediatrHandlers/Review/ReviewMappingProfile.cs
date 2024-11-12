using AutoMapper;
using ECommerceApi.Core.MediatrHandlers.Review.Commands;

namespace ECommerceApi.Core.MediatrHandlers.Review;

public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        CreateMap<Data.Entities.Review, ReviewDto>();
        CreateMap<CreateReviewCommand, Data.Entities.Review>()
            .ForMember(dest => dest.ReviewId, opt => opt.MapFrom(_ => Ulid.NewUlid().ToString()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        CreateMap<UpdateReviewCommand, Data.Entities.Review>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
    }
}